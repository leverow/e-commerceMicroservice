﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ECommerce.Api.Product.Entities;

public class Category
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
