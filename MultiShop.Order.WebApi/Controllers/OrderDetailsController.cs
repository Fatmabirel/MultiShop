using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly GetOrderDetailQueryHandler _getOrderDetailQueryHandler;
        private readonly GetOrderDetailByIdQueryHandler _getOrderDetailByIdQueryHandler;
        private readonly CreateOrderDetailCommandHandler _createOrderDetailCommandHandler;
        private readonly RemoveOrderDetailCommandHandler _removeOrderDetailCommandHandler;
        private readonly UpdateOrderDetailCommandHandler _updateOrderDetailCommandHandler;
        public OrderDetailsController(GetOrderDetailQueryHandler getOrderDetailQueryHandler, GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler, UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler, RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler, CreateOrderDetailCommandHandler createOrderDetailCommandHandler)
        {
            _getOrderDetailQueryHandler = getOrderDetailQueryHandler;
            _getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
            _updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
            _removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;
            _createOrderDetailCommandHandler = createOrderDetailCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailList()
        {
            var values = await _getOrderDetailQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> OrderDetailListById(int id)
        {
            var values = await _getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
        {
            await _createOrderDetailCommandHandler.Handle(command);
            return Ok("Adres başarıyla oluşturuldu");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
        {
            await _updateOrderDetailCommandHandler.Handle(command);
            return Ok("Adres başarıyla güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            await _removeOrderDetailCommandHandler.Handle(new RemoveOrderDetailCommand(id));
            return Ok("OrderDetail başarıyla silindi");
        }
    }
}
