using PassionProject2.Models.ViewModels;
using PassionProject2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject2.Controllers
{
    public class InventoryController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static InventoryController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44302/api/");
        }

        // GET: Inventory/List
        public ActionResult List()
        {
            //objective: communicate with inventory data api to retrieve a list of Inventories
            // curl https://localhost:44302/api/InventoryData/ListInventories

            string url = "InventoryData/ListInventories";

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<InventoryDto> Inventories = response.Content.ReadAsAsync<IEnumerable<InventoryDto>>().Result;
            //Debug.WriteLine("Number of inventory received: ");
            //Debug.WriteLine(Inventories.Count());

            return View(Inventories);
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with inventory data api to retrieve one inventory
            // curl https://localhost:44302/api/InventoryData/FindInventory/{id}

            string url = "InventoryData/FindInventory/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            InventoryDto selectedInventory = response.Content.ReadAsAsync<InventoryDto>().Result;
            //Debug.WriteLine("inventory received: ");
            //Debug.WriteLine(selectedInventory.InventoryName);


            return View(selectedInventory);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Inventory/New
        public ActionResult New()
        {
            //information about all pantry items in the systems
            // curl https://localhost:44302/api/PantryItemData/ListPantryItems

            string url = "PantryItemData/ListPantryItems";

            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PantryItemDto> PantryItemsOptions = response.Content.ReadAsAsync<IEnumerable<PantryItemDto>>().Result;

            return View(PantryItemsOptions);
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult Create(Inventory inventory, int clientTimezoneOffset)
        {
            inventory.InventoryLogDate = inventory.InventoryLogDate.AddMinutes(-clientTimezoneOffset);

            Debug.WriteLine("the json payload is :");
            //objective: add a new inventory into our system using the API

            string url = "InventoryData/AddInventory";

            string jsonpayload = jss.Serialize(inventory);

            Debug.WriteLine(jsonpayload);

            //curl -H "Content-Type:application/json" -d @Inventory.json https://localhost:44302/api/Inventorydata/addInventory 

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateInventory ViewModel = new UpdateInventory();

            string url = "InventoryData/FindInventory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            InventoryDto SelectedInventory = response.Content.ReadAsAsync<InventoryDto>().Result;
            ViewModel.SelectedInventory = SelectedInventory;

            url = "PantryItemData/ListPantryItems/";
            response = client.GetAsync(url).Result;
            IEnumerable<PantryItemDto> PantryItemOptions = response.Content.ReadAsAsync<IEnumerable<PantryItemDto>>().Result;

            ViewModel.PantryItemOptions = PantryItemOptions;

            return View(ViewModel);
        }

        // POST: Inventory/Update/5
        [HttpPost]
        public ActionResult Update(int id, Inventory inventory, int clientTimezoneOffset)
        {
            inventory.InventoryLogDate = inventory.InventoryLogDate.AddMinutes(-clientTimezoneOffset);
            string url = "Inventorydata/updateInventory/" + id;
            string jsonpayload = jss.Serialize(inventory);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Inventory/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Inventorydata/findInventory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            InventoryDto selectedInventory = response.Content.ReadAsAsync<InventoryDto>().Result;
            return View(selectedInventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Inventorydata/deleteInventory/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
