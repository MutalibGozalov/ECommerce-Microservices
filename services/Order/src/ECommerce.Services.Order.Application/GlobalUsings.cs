global using OrderModel = ECommerce.Services.Order.Domain.Entities.Order;
global using ECommerce.Services.Order.Domain.Entities;
global using ECommerce.Services.Order.Application.Common.Mapping;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using ECommerce.Services.Order.Application.Common.Dtos;
global using ECommerce.Shared.Dtos;
global using MediatR;
global using FluentValidation;

global using ECommerce.Services.Order.Application.Common.Interfaces;

global using ECommerce.Services.Order.Application.Order.Commands;
global using ECommerce.Services.Order.Application.Order.Queries;
global using ECommerce.Services.Order.Application.OrderDetailItems.Commands;
global using ECommerce.Services.Order.Application.OrderDetailItems.Queries;
global using ECommerce.Services.Order.Application.ShippingItems.Commands;
global using ECommerce.Services.Order.Application.ShippingItems.Queries;
global using ECommerce.Services.Order.Application.TrackingItems.Queries;
global using ECommerce.Services.Order.Application.TrackingItems.Commands;
