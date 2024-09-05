﻿using API.Domain.Entities;

namespace API.Application.Interfaces;
public interface ITemplateRepository
{
    Task<Template?> GetTemplateByIdAsync(int id);
}
