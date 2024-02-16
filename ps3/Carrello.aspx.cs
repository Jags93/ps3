using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ps3._Default;

namespace ps3
{
    public partial class Carrello : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AggiornaCarrello();
            }
            // Gestisce le azioni del carrello basate sui parametri della query string.
            var action = Request.QueryString["action"];
            var idStr = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int id))
            {
                switch (action)
                {
                    case "add":
                        AggiungiAlCarrello(id);
                        break;
                    case "remove":
                        RimuoviDalCarrello(id);
                        break;
                    case "clear":
                        SvuotaCarrello();
                        break;
                }
                // Aggiorna il carrello e reindirizza alla stessa pagina per prevenire la ripetizione dell'azione con il refresh della pagina.
                AggiornaCarrello();
                Response.Redirect("Carrello.aspx");
            }
        }
        public class CartItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }


        protected void CartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int productId = ((List<CartItem>)Session["Carrello"])[index].Product.IdItem;

            switch (e.CommandName)
            {
                case "UpdateQuantity":
                    TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                    if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
                    {
                        AggiornaQuantita(productId, quantity);
                    }
                    break;
                case "Remove":
                    RimuoviDalCarrello(productId);
                    break;
            }
            AggiornaCarrello();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            SvuotaCarrello();
            AggiornaCarrello();
        }

        private void AggiornaCarrello()
        {
            var carrello = (List<CartItem>)Session["Carrello"];
            CartRepeater.DataSource = carrello;
            CartRepeater.DataBind();

            decimal total = carrello != null ? carrello.Sum(item => item.Product.Price * item.Quantity) : 0;
            lblTotal.Text = $"{total:C}";
        }

        private void AggiornaQuantita(int productId, int quantity)
        {
            var carrello = (List<CartItem>)Session["Carrello"];
            var item = carrello.FirstOrDefault(i => i.Product.IdItem == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }

            Session["Carrello"] = carrello;
            AggiornaCarrello();
        }

        
        private void AggiungiAlCarrello(int productId)
        {
            var carrello = Session["Carrello"] as List<CartItem> ?? new List<CartItem>();
            var prodotti = Session["Catalogo"] as List<Product>;
            var prodotto = prodotti?.FirstOrDefault(p => p.IdItem == productId);
            if (prodotto != null)
            {
                var item = carrello.Find(i => i.Product.IdItem == productId);
                if (item != null)
                {
                    item.Quantity++;
                }
                else
                {
                    carrello.Add(new CartItem { Product = prodotto, Quantity = 1 });
                }
                Session["Carrello"] = carrello;
            }
        }
        // Qai rimuovo i prodotti
        private void RimuoviDalCarrello(int productId)
        {
            var carrello = Session["Carrello"] as List<CartItem>;
            var item = carrello?.FirstOrDefault(i => i.Product.IdItem == productId);
            if (item != null)
            {
                carrello.Remove(item);
                if (carrello.Count == 0)
                {
                    Session.Remove("Carrello");
                }
                else
                {
                    Session["Carrello"] = carrello;
                }
            }
        }
        // svuoto il carello
        private void SvuotaCarrello()
        {
            Session.Remove("Carrello");
        }
    }
}
