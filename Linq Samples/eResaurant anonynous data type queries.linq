<Query Kind="Program">
  <Connection>
    <ID>6ecbcb33-3e1a-4c90-9503-00d17cf707f1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	////anonynous data type queries
	//from food in Items
	//where food.MenuCategory.Description.Equals("Entree")
	//		&& food.Active
	//orderby food.CurrentPrice descending
	//select new //POCOObjectName
	//	{
	//		Description = food.Description,
	//		Price = food.CurrentPrice,
	//		Cost = food.CurrentCost,
	//		Profit = food.CurrentPrice - food.CurrentCost
	//	}
	//
	//from food in Items
	//where food.MenuCategory.Description.Equals("Entree")
	//		&& food.Active
	//orderby food.CurrentPrice descending
	//select new
	//	{
	//		 food.Description,
	//		 food.CurrentPrice,
	//		 food.CurrentCost
	//		//Profit = food.CurrentPrice - food.CurrentCost
	//	}
	
	var result = from food in Items
		where food.MenuCategory.Description.Equals("Entree")
			&& food.Active
		orderby food.CurrentPrice descending
		select new FoodMargins()
		{
			Description = food.Description,
			Price = food.CurrentPrice,
			Cost = food.CurrentCost,
			Profit = food.CurrentPrice - food.CurrentCost
		};
	result.Dump();
	
	//get all the bills and bill items for waiters in Sep of 2014.
	//only those bills which were paid.
	var result2 = from orders in Bills
					where orders.PaidStatus &&
					(orders.BillDate.Month == 9 && orders.BillDate.Year == 2014)
					orderby orders.Waiter.LastName, orders.Waiter.FirstName
					select new
					{
						BillID = orders.BillID,
						WaiterName = orders.Waiter.LastName + ", " + orders.Waiter.FirstName,
						Orders = orders.BillItems
					};

	result2.Dump();
}

// Define other methods and classes here

//this is a POCO class
public class FoodMargins
{
	public string Description {get;set;}
	public decimal Price {get;set;}
	public decimal Cost {get;set;}
	public decimal Profit {get;set;}
}

//this is a DTO class
public class BillOrders
{
	public int BillID {get;set;}
	public string WaiterName {get;set;}
//	public BillItems Orders {get;set;}
	public IEnumerable Orders {get;set;}
	
}