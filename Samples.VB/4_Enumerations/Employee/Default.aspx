<%@ Page Title="EmployeeList" Language="VB" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.vb" Inherits="Samples._Default5" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Employees List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>
    <div>
        <asp:ListView id="ListView1" runat="server"
            DataKeyNames="Id" 
			ItemType="Samples.Employee"
            SelectMethod="GetData">
            <EmptyDataTemplate>
                There are no entries found for Employees
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>
								<asp:DynamicControl runat="server" DataField="Id" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Email" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="HomePage" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Salary" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Region" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager PageSize="5"  runat="server">
					<Fields>
                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                        <asp:NumericPagerField ButtonType="Button"  NumericButtonCssClass="btn" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn" />
                    </Fields>
				</asp:DataPager>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
							<td>
								<asp:DynamicControl runat="server" DataField="Id" ID="Id" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Email" ID="Email" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="HomePage" ID="HomePage" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Salary" ID="Salary" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Region" ID="Region" Mode="ReadOnly" />
							</td>
                    <td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/4_Enumerations/Employee/Edit", Item.Id) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/4_Enumerations/Employee/Delete", Item.Id) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

