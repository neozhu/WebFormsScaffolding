
<%@ Page Title="EmployeeDelete" Language="C#" MasterPageFile="~/Site.Master" CodeBehind="Delete.aspx.cs" Inherits="Samples._4_Enumerations.Employee.Delete" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
		<p>&nbsp;</p>
        <h3>Are you sure want to delete this Employee?</h3>
        <asp:FormView runat="server"
            ItemType="Samples.Enumerations.Employee" DataKeyNames="Id"
            DeleteMethod="DeleteItem" SelectMethod="GetItem"
            OnItemCommand="ItemCommand" RenderOuterTable="false">
            <EmptyDataTemplate>
                Cannot find the Employee with Id <%: Request.QueryString["Id"] %>
            </EmptyDataTemplate>
            <ItemTemplate>
                <fieldset class="form-horizontal">
                    <legend>Delete Employee</legend>

							<div class="row">
								<div class="col-sm-2 text-right">
									<strong><asp:DynamicControl runat="server" DataField="Id" Mode="ReadOnly" UIHint="FieldLabel" /></strong>
								</div>
								<div class="col-sm-2">
									<asp:DynamicControl runat="server" DataField="Id" ID="Id" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong><asp:DynamicControl runat="server" DataField="Email" Mode="ReadOnly" UIHint="FieldLabel" /></strong>
								</div>
								<div class="col-sm-2">
									<asp:DynamicControl runat="server" DataField="Email" ID="Email" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong><asp:DynamicControl runat="server" DataField="HomePage" Mode="ReadOnly" UIHint="FieldLabel" /></strong>
								</div>
								<div class="col-sm-2">
									<asp:DynamicControl runat="server" DataField="HomePage" ID="HomePage" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong><asp:DynamicControl runat="server" DataField="Salary" Mode="ReadOnly" UIHint="FieldLabel" /></strong>
								</div>
								<div class="col-sm-2">
									<asp:DynamicControl runat="server" DataField="Salary" ID="Salary" Mode="ReadOnly" />
								</div>
							</div>
							<div class="row">
								<div class="col-sm-2 text-right">
									<strong><asp:DynamicControl runat="server" DataField="Region" Mode="ReadOnly" UIHint="FieldLabel" /></strong>
								</div>
								<div class="col-sm-2">
									<asp:DynamicControl runat="server" DataField="Region" ID="Region" Mode="ReadOnly" />
								</div>
							</div>


                 	<div class="row">
					  &nbsp;
					</div>
					<div class="form-group">
						<div class="col-sm-offset-2 col-sm-10">
							<asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger" />
							<asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-default" />
						</div>
					</div>
                </fieldset>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>


