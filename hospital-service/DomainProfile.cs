using AutoMapper;

namespace HospitalService
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Contracts.V1.Address, Contracts.V2.Address>()
                .ForMember(d => d.City, o => o.MapFrom(s => s.Place))
                .ForMember(d => d.Email, o => o.Ignore())
                .ForMember(d => d.Fax, o => o.Ignore())
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Latitude))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Longitude))
                .ForMember(d => d.Phone, o => o.Ignore())
                .ForMember(d => d.Postcode, o => o.MapFrom(s => s.Postcode))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Street))
                .ForMember(d => d.StreetNr, o => o.MapFrom(s => s.StreetNr))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url));

            CreateMap<Contracts.V1.TLHOWImageUrl, Contracts.V2.Picture>()
                .ForMember(d => d.SizeL, o => o.MapFrom(s => s.GetUrl("l")))
                .ForMember(d => d.SizeM, o => o.MapFrom(s => s.GetUrl("m")))
                .ForMember(d => d.SizeS, o => o.MapFrom(s => s.GetUrl("s")))
                .ForMember(d => d.SizeXL, o => o.MapFrom(s => s.GetUrl("xl")))
                .ForMember(d => d.SizeXS, o => o.MapFrom(s => s.GetUrl("xs")))
                .ForMember(d => d.Thumbnail, o => o.MapFrom(s => s.GetUrl("thumb")));

            CreateMap<Contracts.V1.TLHOWChiefDoctor, Contracts.V2.Doctor>()
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.CVUrl, o => o.MapFrom(s => s.CV))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.GUID, o => o.Ignore())
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Pictures, o => o.MapFrom(s => s.Images))
                .ForMember(d => d.Salutation, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Specialty, o => o.MapFrom(s => s.Function));

            CreateMap<Contracts.V1.TLHOWAddress, Contracts.V2.Address>()
                .ForMember(d => d.City, o => o.MapFrom(s => s.Place))
                .ForMember(d => d.Email, o => o.Ignore())
                .ForMember(d => d.Fax, o => o.Ignore())
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Latitude))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Longitude))
                .ForMember(d => d.Phone, o => o.Ignore())
                .ForMember(d => d.Postcode, o => o.MapFrom(s => s.Postcode))
                .ForMember(d => d.Street, o => o.MapFrom(s => s.Street))
                .ForMember(d => d.StreetNr, o => o.MapFrom(s => s.StreetNr))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url));

            CreateMap<Contracts.V1.Hospital, Contracts.V2.Hospital>()
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.Departments, o => o.MapFrom(s => s.Departments))
                .ForMember(d => d.GUID, o => o.MapFrom(s => s.IK))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Pictures, o => o.MapFrom(s => s.CurrentDepartment.TLHOW != null ? s.CurrentDepartment.TLHOW.ImageGallery : null))
                .ForMember(d => d.SortOrder, o => o.Ignore())
                .ForMember(d => d.DistanceFromLocationKm, o => o.MapFrom(s => s.DistanceFromLocation));

            CreateMap<Contracts.V1.Department, Contracts.V2.Department>()
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.Certificates, o => o.MapFrom(s => s.TLHOW != null ? s.TLHOW.Certificates : null))
                .ForMember(d => d.DepartmentClassification, o => o.MapFrom(s => string.Join(",", s.ClassificationNumbers)))
                .ForMember(d => d.DescriptionHtml, o => o.MapFrom(s => s.TLHOW != null ? s.TLHOW.DepartmentDescription : null))
                .ForMember(d => d.Doctors, o => o.MapFrom(s => s.TLHOW != null ? s.TLHOW.ChiefDoctors : null))
                .ForMember(d => d.GUID, o => o.MapFrom(s => s.MakeKey))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Pictures, o => o.MapFrom(s => s.TLHOW != null ? s.TLHOW.ImageGallery : null))
                .ForMember(d => d.SortOrder, o => o.Ignore())
                .ForMember(d => d.WorktimeMessageHtml, o => o.MapFrom(s => s.TLHOW != null ? s.TLHOW.WorkingTime : null));

            CreateMap<Contracts.V1.TLHOWCert, Contracts.V2.Certificate>()
                .ForMember(d => d.ExternalUrl, o => o.MapFrom(s => s.SiteUrl))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Text))
                .ForMember(d => d.Pictures, o => o.MapFrom(s => s.Images));
        }
    }
}
