<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="ps3.Carrello" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carrello</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="CartRepeater" runat="server">
            <ItemTemplate>
                <div>
                    <img src='<%# Eval("Product.Image") %>' alt='<%# Eval("Product.Name") %>' style="width:100px;height:100px;">
                    <h4><%# Eval("Product.Name") %></h4>
                    Quantità: <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' Width="50"></asp:TextBox>
                    <asp:Button ID="btnUpdateQuantity" runat="server" Text="Aggiorna" CommandName="UpdateQuantity" CommandArgument='<%# Container.ItemIndex %>' />
                    <p>Prezzo: <%# Eval("Product.Price", "{0:C}") %></p>
                    <asp:LinkButton ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("Product.IdItem") %>' Text="Rimuovi"></asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        Totale: <asp:Label ID="lblTotal" runat="server"></asp:Label>
        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Svuota Carrello" />
    </form>
</body>
</html>
