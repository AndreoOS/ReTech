﻿using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage CustomerNotFound => new(HttpStatusCode.NotFound, "Customer doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage OrderNotFound => new(HttpStatusCode.NotFound, "Order doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProductNotFound => new(HttpStatusCode.NotFound, "Product doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage CategoryNotFound => new(HttpStatusCode.NotFound, "Category doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage BrandNotFound => new(HttpStatusCode.NotFound, "Brand doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage CartNotFound => new(HttpStatusCode.NotFound, "Cart doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProductBrandNotFound => new(HttpStatusCode.NotFound, "ProductBrand doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
