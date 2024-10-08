﻿using API.Application.Enums;

namespace API.Application.Interfaces;
public interface IBulkSendEmailUseCase
{
    Task ExecuteAsync(Stream xmlStream, EmailXMLVerificationType verificationType);
}
