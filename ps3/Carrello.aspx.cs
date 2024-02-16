
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
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
                AggiornaCarrello();
                Response.Redirect("Carrello.aspx"); // Per evitare duplicazioni nell'aggiornamento della pagina
            }
        }
        public class CartItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }

        private void AggiornaCarrello()
        {
            var carrello = Session["Carrello"] as List<CartItem>;
            CartRepeater.DataSource = carrello;
            CartRepeater.DataBind();
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

        private void SvuotaCarrello()
        {
            Session.Remove("Carrello");
        }
    }
}