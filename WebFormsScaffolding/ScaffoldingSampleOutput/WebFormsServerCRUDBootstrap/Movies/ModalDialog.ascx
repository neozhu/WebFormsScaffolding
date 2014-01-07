<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalDialog.ascx.cs" Inherits="WebFormsServerCRUDBootstrap.Movies.DetailsDialog" %>
<%@ Register TagPrefix="user" TagName="Update" src="Update.ascx" %>
<%@ Register TagPrefix="user" TagName="Details" src="Details.ascx" %>

  <div class="modal fade" id="detailsDialog" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">

    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="MoviesList" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <asp:FormView ItemType="WebFormsServerCRUDBootstrap.Models.Movie" SelectMethod="Unnamed_GetItem" runat="server">
            <ItemTemplate>
                <user:Details runat="server" />
            </ItemTemplate>
                <EditItemTemplate>
                    <user:Update Runat="server" />
                </EditItemTemplate>
            </asp:FormView>




        </ContentTemplate>
    </asp:UpdatePanel>


  </div>
</div>