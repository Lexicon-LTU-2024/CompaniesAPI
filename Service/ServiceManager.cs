﻿using AutoMapper;
using Domain.Contracts;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;

    public ServiceManager(Lazy<ICompanyService> companyService, Lazy<IEmployeeService> employeeService )
    {
        _companyService = companyService;
        _employeeService = employeeService;
    }
}
