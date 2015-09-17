<Query Kind="Expression">
  <Connection>
    <ID>6ecbcb33-3e1a-4c90-9503-00d17cf707f1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//orderby
from food in Items
orderby food.Description
select food

//also available descending
from food in Items
orderby food.CurrentPrice descending
select food

//can use both ascending and descending
from food in Items
orderby food.CurrentPrice descending, food.Calories ascending
select food

//you can use where and orderby together
from food in Items
orderby food.CurrentPrice descending, food.Calories ascending
where food.MenuCategory.Description.Equals("Entree")
select food

