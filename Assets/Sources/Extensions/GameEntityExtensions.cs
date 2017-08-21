﻿using System;
using Assets.Sources.Features.Items;
using Assets.Sources.Helpers.Items;

public static class GameEntityExtensions
{
	public static InventoryItem GetInventoryItem(this GameEntity entity, InventorySlot slot)
	{
		if (!entity.hasInventory)
		{
			return null;
		}

		var items = entity.inventory.Items;
		InventoryItem item;
		items.TryGetValue(slot, out item);

		return item;
	}

	public static Weapon GetWeapon(this GameEntity entity)
	{
		var item = entity.GetInventoryItem(InventorySlot.Weapon);
		return item != null ? (Weapon) item.Item : null;
	}

	public static void SetInventoryItem(this GameEntity entity, InventorySlot slot, IItem item, int count)
	{
		if (!entity.hasInventory)
		{
			throw new InvalidOperationException();
		}

		var items = entity.inventory.Items;
		items[slot] = new InventoryItem(item, count);
	}

	public static void SetInventoryItem(this GameEntity entity, InventorySlot slot, IItem item)
	{
		entity.SetInventoryItem(slot, item, 1);
	}

	public static EntityReference GetReference(this GameEntity entity)
	{
		if (!entity.hasNetworkTracked)
		{
			throw new ArgumentException("Entity is not network tracked");
		}

		return entity.networkTracked.Reference;
	}
}