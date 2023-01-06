using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAttribute<T> : IValue<T> where T : struct
{
	string Output { get; }

	string LocalizationKey { get; }
}

public abstract class Attribute<T> : IAttribute<T> where T : struct
{
	public event UnityAction onChanged;

	public virtual string Output => CurrentValue.ToString();

	public virtual string LocalizationKey => "sheet.";

	public virtual T CurrentValue
	{
		get => currentValue;
		set
		{
			currentValue = value;
			onChanged?.Invoke();
		}
	}
	protected T currentValue;

	public Attribute(T currentValue)
	{
		this.currentValue = currentValue;
	}
}

public abstract class AttributeBar : Attribute<float>, IBar<float>
{
	public override string Output => $"{Math.Round(CurrentValue)} / {Math.Round(MaxValue)}";

	public override float CurrentValue
	{
		get => currentValue;
		set
		{
			base.CurrentValue = Mathf.Clamp(value, MinValue, MaxValue);
		}
	}

	public virtual float MaxValue
	{
		get => maxValue;
		set
		{
			maxValue = value;
			base.CurrentValue = Mathf.Clamp(currentValue, MinValue, MaxValue);
		}
	}
	protected float maxValue;

	public virtual float MinValue { get; protected set; }

	public float PercentValue => CurrentValue / MaxValue;

	protected AttributeBar(float value, float min, float max) : base(value)
	{
		this.maxValue = max;
		this.MinValue = min;
		this.CurrentValue = value;
	}
}

public abstract class AttributeModifiableFloat : Attribute<float>, IModifiable<AttributeModifier, float>, IModifiablePercent
{
	public event UnityAction onModifiersChanged;

	public override string Output => $"{TotalValue}";

	public virtual float TotalValue => (CurrentValue + ModifyAddValue) * (1f + (ModifyPercentValue / 100f));

	public virtual float ModifyAddValue
	{
		get
		{
			float value = 0;

			Modifiers.ForEach((modifier) =>
			{
				if (modifier is AddModifier add)
				{
					value += add.CurrentValue;
				}
			});

			return value;
		}
	}

	public virtual float ModifyPercentValue
	{
		get
		{
			float value = 0;

			Modifiers.ForEach((modifier) =>
			{
				if (modifier is PercentModifier percent)
				{
					value += percent.CurrentValue;
				}
			});

			return value;
		}
	}

	public List<IModifier> Modifiers { get; }

	public AttributeModifiableFloat(float value) : base(value)
	{
		Modifiers = new List<IModifier>();
	}

	public virtual bool AddModifier(AttributeModifier modifier)
	{
		if (!Contains(modifier))
		{
			Modifiers.Add(modifier);

			modifier.onChanged += OnModifierChanged;

			onModifiersChanged?.Invoke();

			return true;
		}

		return false;
	}

	public virtual bool RemoveModifier(AttributeModifier modifier)
	{
		if (Contains(modifier))
		{
			Modifiers.Remove(modifier);

			modifier.onChanged -= OnModifierChanged;

			onModifiersChanged?.Invoke();

			return true;
		}

		return false;
	}

	public bool Contains(IModifier modifier) => Modifiers.Contains(modifier);

	private void OnModifierChanged()
	{
		onModifiersChanged?.Invoke();
	}
}