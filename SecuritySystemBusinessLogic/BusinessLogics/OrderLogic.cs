using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.BusinessLogicsContracts;
using SecuritySystemContracts.Enums;
using SecuritySystemContracts.StoragesContracts;
using SecuritySystemContracts.ViewModels;
using SecuritySystemContracts.Enums;


namespace SecuritySystemBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                KitId = model.KitId,
                KitName = model.KitName,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят,
                DateImplement = DateTime.Now
            }); ;
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status.ToString() != "Принят")
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                KitId = order.KitId,
                KitName = order.KitName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status.ToString() != "Выполняется")
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                KitId = order.KitId,
                KitName = order.KitName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }
        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status.ToString() != "Готов")
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }

            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                KitId = order.KitId,
                KitName = order.KitName,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан
            });

        }
    }
}

