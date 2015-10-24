<Query Kind="Expression">
  <Connection>
    <ID>6ecbcb33-3e1a-4c90-9503-00d17cf707f1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from abillrow in Bills
where abillrow.BillDate.Month == 5
orderby abillrow.BillDate,
		abillrow.Waiter.LastName,
		abillrow.Waiter.FirstName
select new
{
	BillDate = new DateTime(abillrow.BillDate.Year,
							abillrow.BillDate.Month,
							abillrow.BillDate.Day),
							
	WaiterName = abillrow.Waiter.LastName + "," +  abillrow.Waiter.FirstName,
	BillID = abillrow.BillID,
	BillTotal = abillrow.BillItems.Sum(eachbillitemrow  => eachbillitemrow.Quantity * eachbillitemrow.SalePrice),
	PartySize = abillrow.NumberInParty,
	Contact = abillrow.Reservation.CustomerName
}