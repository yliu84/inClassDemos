<Query Kind="Expression">
  <Connection>
    <ID>6ecbcb33-3e1a-4c90-9503-00d17cf707f1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//groupby

from food in Items
group food by food.MenuCategory.Description

//This creates a key with a value and the row collection for that key value

//more than one field
from food in Items
group food by new {food.MenuCategory.Description, food.CurrentPrice}