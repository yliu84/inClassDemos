<Query Kind="Expression">
  <Connection>
    <ID>6ecbcb33-3e1a-4c90-9503-00d17cf707f1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Where clause

//List all tables that hold more that 3 people
//query syntax

from row in Tables
where row.Capacity > 3
select row

//method syntax
Tables.Where(row => row.Capacity > 3)

//list all items with more that 500 calories
Items.Where(row => row.Calories > 500)

from food in Items
where food.Calories > 500
select food

//list all items with more that 500 calories and selling for 
//more than $10.00
from food in Items
where food.Calories > 500 && food.CurrentPrice > 10.00m
select food

//list all items with more that 500 clories and are Entrees on the menu
//HINT: navigational properties of the database are known by LinqPad.
from food in Items
where food.Calories > 500 &&
	  food.MenuCategory.Description.Equals("Entree")
select food
