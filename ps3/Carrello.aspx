<asp:Repeater ID="CartRepeater" runat="server">
    <ItemTemplate>
        <div>
            <img src='<%# Eval("Product.Image") %>' alt='<%# Eval("Product.Name") %>' style="width:100px;height:100px;">
            <h4><%# Eval("Product.Name") %></h4>
            <p>Quantità: 
                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' Width="50"></asp:TextBox>
                <asp:Button ID="btnUpdateQuantity" runat="server" Text="Aggiorna" CommandName="UpdateQuantity" CommandArgument='<%# Eval("Product.idItem") %>' />
            </p>
            <p>Prezzo: <%# Eval("Product.Price", "{0:C}") %></p>
            <a href='Carrello.aspx?action=remove&id=<%# Eval("Product.idItem") %>'>Rimuovi</a>
        </div>
    </ItemTemplate>
</asp:Repeater>
<div>
    Totale: <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
</div>
<a href='Carrello.aspx?action=clear'>Svuota Carrello</a>
