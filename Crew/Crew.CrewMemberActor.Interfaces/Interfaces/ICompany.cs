using System;

namespace Crew.CrewMemberActor.Interfaces.Interfaces
{
    public interface ICompany
    {
        String Name { get; set; }
        String Address { get; set; }
        Int32 Telephone { get; set; }
        CompanyType CompanyType { get; set; }
    }

    public enum CompanyType
    {
        Corporations,
        LimitedLiabilityCompanies
    }
}
