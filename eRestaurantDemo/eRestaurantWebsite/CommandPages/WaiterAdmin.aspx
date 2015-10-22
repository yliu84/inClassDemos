﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WaiterAdmin.aspx.cs" Inherits="CommandPages_WaiterAdmin" %>

<%@ Register Src="~/UserControl/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br /><br /><br />
    <h1>Waiter Admin CRUD</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Waiter Names"></asp:Label>
    <asp:DropDownList ID="WaiterList" runat="server" DataSourceID="ObjectDataSource1" DataTextField="FullName" DataValueField="WaiterID">
        
    </asp:DropDownList>
    <asp:LinkButton ID="FetchWaiter" runat="server" OnClick="FetchWaiter_Click">Fetch Waiter</asp:LinkButton>
    <table align="center" style="width: 70%">
        <tr>
            <td>ID</td>
            <td>
                <asp:Label ID="WaiterID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>First Name</td>
            <td>
                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td>
                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Address</td>
            <td>
                <asp:TextBox ID="Address" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Phone</td>
            <td>
                <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Hire Date</td>
            <td>
                <asp:TextBox ID="HireDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Release Date</td>
            <td>
                <asp:TextBox ID="ReleaseDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="WaiterInsert" runat="server" OnClick="WaiterInsert_Click">Insert</asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton ID="WaiterUpdate" runat="server" OnClick="WaiterUpdate_Click">Update</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="eRestaurantSystem.DAL.Entities.Waiter" DeleteMethod="Waiter_Delete" InsertMethod="Waiter_Add" OldValuesParameterFormatString="original_{0}" SelectMethod="Waiters_List" TypeName="eRestaurantSystem.BLL.AdminController" UpdateMethod="Waiter_Update" OnInserted="CheckForException"></asp:ObjectDataSource>
</asp:Content>

