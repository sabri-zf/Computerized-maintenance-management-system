using computrized_maintenance_Data_Access.Entites.AssetsManagment;
using computrized_maintenance_Data_Access.Entites.DownTimeTracking;
using computrized_maintenance_Data_Access.Entites.InventoryManagement;
using computrized_maintenance_Data_Access.Entites.preventiveMaintenanceManagement;
using computrized_maintenance_Data_Access.Entites.WorkOrderManagement;
using computrized_maintenance_Data_Access.Enumes;
using System.Collections.ObjectModel;

namespace computrized_maintenance_Data_Access.Data.Seed
{
	/// <summary>
	/// Created Collection of Seed-data to each entity on System
	/// </summary>
	internal class SeeData
	{

		public static Collection<PreventiveMaintenance> PreventiveMaintenances = new Collection<PreventiveMaintenance>()
		{
			new PreventiveMaintenance() {
				ID=1,
				AssetID=3,
				TaskDescription="Replace air filter in HVAC unit every 3 months.",
				Frequency=EnFrequencyTask.Quarterly,
				CreatedDate=new DateTime(2025,1,1),
				NextDueDate=new DateTime(2025,10,1)
			},
			new PreventiveMaintenance() {
				ID=2,
				AssetID=2,
				TaskDescription="Inspect safety valves on boiler system every 6 months.",
				Frequency= EnFrequencyTask.SemiAnnual,
				CreatedDate=new DateTime(2025,3,1),
				NextDueDate=new DateTime(2025,9,1)
			}

		};

		public static Collection<WorkOrder> WorkOrders = new Collection<WorkOrder>()
		{
			  new WorkOrder
		{
			ID = 1,
			WorkOrderNumber = "WO-0001",
			Description = "Replace air filter in HVAC unit.",
			AssetID = 3,
			PreventiveMaintenanceID = 1,
			CreatedByID = 22,
			AssignedToID = 1,
			Status = workOrderStatus.Open,
			CreatedDate = new DateTime(2025, 10, 1),
			StartDate = new DateTime(2025, 10, 3),
			DueDate = new DateTime(2025, 10, 7),
			CompeletedDate = null,
			Note = "Check stock for spare filters before starting."
		},

		new WorkOrder
		{
			ID = 2,
			WorkOrderNumber = "WO-0002",
			Description = "Inspect safety valves on boiler system.",
			AssetID = 2,
			PreventiveMaintenanceID = 2,
			CreatedByID = 5,
			AssignedToID = 1,
			Status = workOrderStatus.Completed,
			CreatedDate = new DateTime(2025, 9, 15),
			StartDate = new DateTime(2025, 9, 17),
			DueDate = new DateTime(2025, 9, 18),
			CompeletedDate = new DateTime(2025, 9, 17),
			Note = "All valves passed inspection."
		}
		};

		public static Collection<WorkOrderHistory> WorkOrderHistorys = new Collection<WorkOrderHistory>()
		{
			new WorkOrderHistory
			{
				ID = 1,
				WO_ID = 1,
				Action = WorkOrderHistoryActionStatus.Created,
				ActionDate = new DateTime(2025, 10, 1),
				PerformedActionByID = 22
			},
			new WorkOrderHistory
			{
				ID = 2,
				WO_ID = 1,
				Action = WorkOrderHistoryActionStatus.Assgined,
				ActionDate = new DateTime(2025, 10, 2),
				PerformedActionByID = 22
			},
			new WorkOrderHistory
			{
				ID = 3,
				WO_ID = 2,
				Action = WorkOrderHistoryActionStatus.Created,
				ActionDate = new DateTime(2025, 9, 15),
				PerformedActionByID = 5
			},
			new WorkOrderHistory
			{
				ID = 4,
				WO_ID = 2,
				Action = WorkOrderHistoryActionStatus.Completed,
				ActionDate = new DateTime(2025, 9, 17),
				PerformedActionByID = 1
			}
		};

		public static Collection<WorkOrderPart> WorkOrderParts = new Collection<WorkOrderPart>()
		{
			new WorkOrderPart
			{
				ID = 1,
				WO_ID = 1,
				PartItemID = 2,
				QuantityUsed = 2
			},
			new WorkOrderPart
			{
				ID = 2,
				WO_ID = 1,
				PartItemID = 5,
				QuantityUsed = 1
			},
			new WorkOrderPart
			{
				ID = 3,
				WO_ID = 2,
				PartItemID = 1,
				QuantityUsed = 3
			}
		};

		public static Collection<InventoryItem> InventoryItems = new Collection<InventoryItem>()
		{
			  new InventoryItem
	{
		ID = 1,
		ItemName = "Air Filter",
		PartNumber = "AF-1001",
		Description = "Standard HVAC air filter, 20x25x2 inches.",
		Quintity = 47,
		ReorderLevel = 10,
		UnitCost = 12.50m,
		LocationID = 1,
		IsActive = true
	},
	new InventoryItem
	{
		ID = 2,
		ItemName = "Hydraulic Hose",
		PartNumber = "HH-2045",
		Description = "High-pressure hydraulic hose, 2-meter length.",
		Quintity = 18,
		ReorderLevel = 5,
		UnitCost = 45.75m,
		LocationID = 2,
		IsActive = true
	},
	new InventoryItem
	{
		ID = 3,
		ItemName = "Lubricant Oil",
		PartNumber = "LO-3020",
		Description = "Synthetic lubricant oil, 5-liter container.",
		Quintity = 15,
		ReorderLevel = 5,
		UnitCost = 38.90m,
		LocationID = 1,
		IsActive = true
	},
	new InventoryItem
	{
		ID = 4,
		ItemName = "Safety Valve",
		PartNumber = "SV-1550",
		Description = "Industrial safety valve, 2-inch diameter.",
		Quintity = 0,
		ReorderLevel = 3,
		UnitCost = 120.00m,
		LocationID = 3,
		IsActive = false
	},
	new InventoryItem
	{
		ID = 5,
		ItemName = "Conveyor Belt Motor",
		PartNumber = "CBM-450",
		Description = "Replacement motor for conveyor belt system.",
		Quintity = 1,
		ReorderLevel = 1,
		UnitCost = 350.00m,
		LocationID = 2,
		IsActive = true
	},
	new InventoryItem
	{
		ID = 6,
		ItemName = "Pressure Sensor",
		PartNumber = "PS-8812",
		Description = "Digital pressure sensor, 0–10 bar range.",
		Quintity = 0,
		ReorderLevel = 4,
		UnitCost = 78.40m,
		LocationID = 3,
		IsActive = false
	}
		};

		public static Collection<Location> Locations = new Collection<Location>()
		{
			new Location
			{
				ID = 1,
				LocationName = "Main Warehouse",
			},
			new Location
			{
				ID = 2,
				LocationName = "Secondary Storage",
			},
			new Location
			{
				ID = 3,
				LocationName = "Outdoor Yard",
			}
		};

		public static Collection<InventoryTransaction> InventoryTransactions = new Collection<InventoryTransaction>()
		{
			 new InventoryTransaction
	{
		ID = 1,
		InventoryItemID = 1,
		Type = TransactionType.In,
		Quntity = 100,
		TransactionDate = new DateTime(2025, 9, 25),
	},
	new InventoryTransaction
	{
		ID = 2,
		InventoryItemID = 1,
		Type = TransactionType.Out,
		Quntity = 20,
		TransactionDate = new DateTime(2025, 10, 3),
		Reference = "WO-0001"
	},
	new InventoryTransaction
	{
		ID = 3,
		InventoryItemID = 2,
		Type = TransactionType.In,
		Quntity = 10,
		TransactionDate = new DateTime(2025, 9, 15),
	},
	new InventoryTransaction
	{
		ID = 4,
		InventoryItemID = 2,
		Type = TransactionType.Out,
		Quntity = 3,
		TransactionDate = new DateTime(2025, 10, 6),
	},
	new InventoryTransaction
	{
		ID = 5,
		InventoryItemID = 3,
		Type = TransactionType.Out,
		Quntity = 2,
		TransactionDate = new DateTime(2025, 10, 12),
	},

		};

		public static Collection<DownTimeEvent> downTimeEvents = new Collection<DownTimeEvent>()
		{
			  new DownTimeEvent
	{
		ID = 1,
		AssetID = 1,
		WO_ID = 1,
		DownTimeType = EnDownTimeType.Unplanned,
		StartDownTimeEvent = new DateTime(2025, 10, 10, 8, 15, 0),
		EndDownTimeEvent = new DateTime(2025, 10, 10, 10, 45, 0),
		Reason = "Motor overheating due to lack of lubrication",
		ActionTaken = "Replaced bearing and added lubrication",
		PerformedByID = 1
	},

	new DownTimeEvent
	{
		ID = 2,
		AssetID = 3,
		WO_ID = 2,
		DownTimeType = EnDownTimeType.Planned,
		StartDownTimeEvent = new DateTime(2025, 10, 12, 14, 00, 0),
		EndDownTimeEvent = new DateTime(2025, 10, 12, 17, 30, 0),
		Reason = "Scheduled control system upgrade",
		ActionTaken = "Installed new firmware and tested functionality",
		PerformedByID = 6
	},
	new DownTimeEvent
	{
		ID = 3,
		AssetID = 3,
		WO_ID = null,
		DownTimeType = EnDownTimeType.Unplanned,
		StartDownTimeEvent = new DateTime(2025, 10, 15, 9, 00, 0),
		EndDownTimeEvent = new DateTime(2025, 10, 15, 9, 45, 0),
		Reason = "Unexpected sensor calibration issue",
		ActionTaken = "Recalibrated sensor and updated firmware",
		PerformedByID = 6
	}
		};

	}
}
