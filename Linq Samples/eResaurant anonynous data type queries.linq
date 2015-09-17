<Query Kind="Expression">
  <Connection>
    <ID>6ecbcb33-3e1a-4c90-9503-00d17cf707f1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//anonynous data type queries
from food in Items
where food.MenuCategory.Description.Equals("Entree")
		&& food.Active
orderby food.CurrentPrice descending
select new //POCOObjectName
	{
		Description = food.Description,
		Price = food.CurrentPrice,
		Cost = food.CurrentCost,
		Profit = food.CurrentPrice - food.CurrentCost
	}

from food in Items
where food.MenuCategory.Description.Equals("Entree")
		&& food.Active
orderby food.CurrentPrice descending
select new
	{
		 food.Description,
		 food.CurrentPrice,
		 food.CurrentCost
		//Profit = food.CurrentPrice - food.CurrentCost
	}