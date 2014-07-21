<%@ Page Title="MovieList" Language="VB" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.vb" Inherits="1_Simple.Movie._Default1" %>
<%@ Register TagPrefix="FriendlyUrls" Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Movies List</h2>
    <p>
        <asp:HyperLink runat="server" NavigateUrl="Insert" Text="Create new" />
    </p>
    <div>
        <asp:ListView id="ListView1" runat="server"
            DataKeyNames="Id" 
			ItemType=".Movie"
            SelectMethod="GetData">
            <EmptyDataTemplate>
                There are no entries found for Movies
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>
								<asp:DynamicControl runat="server" DataField="Id" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Title" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Director" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Comments" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Count" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="Price" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>
								<asp:DynamicControl runat="server" DataField="ReleaseDate" Mode="ReadOnly" UIHint="FieldLabel" />
							</th>
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                </table>
				<asp:DataPager PageSize="5" runat="server">
					<fields>
						<asp:NumericPagerField ButtonType="Link" />
					</fields>
				</asp:DataPager>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
							<td>
								<asp:DynamicControl runat="server" DataField="Id" ID="Id" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Title" ID="Title" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Director" ID="Director" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Comments" ID="Comments" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Count" ID="Count" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="Price" ID="Price" Mode="ReadOnly" />
							</td>
							<td>
								<asp:DynamicControl runat="server" DataField="ReleaseDate" ID="ReleaseDate" Mode="ReadOnly" />
							</td>
                    <td>
					    <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/1_Simple/Movie/Edit", Item.Id) %>' Text="Edit" /> | 
                        <asp:HyperLink runat="server" NavigateUrl='<%# FriendlyUrl.Href("~/1_Simple/Movie/Delete", Item.Id) %>' Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

