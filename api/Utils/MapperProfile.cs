using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Education;
using api.Dto.ExamSchedule;
using api.Dto.Feeds;
using api.Dto.Major;
using api.Dto.Student;
using api.Dto.Subject;
using api.Entity;
using AutoMapper;

namespace api.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateStudentDto, Student>().ReverseMap();
            CreateMap<UpdateStudentDto, Student>()
             .ForMember(dest => dest.Role, opt => opt.Ignore()) // Bỏ qua , Role, MajorId
             .ForMember(dest => dest.MajorId, opt => opt.Ignore())
             .ReverseMap();
            CreateMap<Student, StudentDto>()
            .ForMember(studentDto => studentDto.MajorName, opt => opt.MapFrom(student => student.Major.MajorName))
            .ReverseMap();


            CreateMap<Subject, SubjectDto>()
             .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentSubjects.Select(ss => ss.Student)));
            CreateMap<CreateSubjectDto, Subject>().ReverseMap();
            CreateMap<UpdateSubjectDto, Subject>().ReverseMap();

            CreateMap<ExamSchedule, ExamScheduleDto>()
             .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.ExamScheduleSubjects.Select(es => es.Subject)));
            CreateMap<CreateExampleScheduleDto, ExamSchedule>().ReverseMap();
            CreateMap<UpdateExampleScheduleDto, ExamSchedule>().ReverseMap();
            
            CreateMap<MajorDto, Major>().ReverseMap();
            CreateMap<CreateMajorDto, Major>().ReverseMap();
            CreateMap<UpdateMajorDto, Major>().ReverseMap();




            CreateMap<Education, EducationDto>()
             .ForMember(educationDto => educationDto.StudentName, opt => opt.MapFrom(education => education.Student.FullName))
             .ForMember(education => education.MajorName, opt => opt.MapFrom(education => education.Major.MajorName))
             .ReverseMap();
            CreateMap<CreateEducationDto, Education>().ReverseMap();
            CreateMap<UpdateEducationDto, Education>().ReverseMap();





            CreateMap<NewsFeed, NewFeedDto>()
             .ForMember(newsFeedDto => newsFeedDto.StudentName, opt => opt.MapFrom(newsFeed => newsFeed.Student.FullName))
             .ReverseMap();
            CreateMap<CreateFeedDto, NewsFeed>().ReverseMap();
            CreateMap<UpdateFeedDto, NewsFeed>()
             .ForMember(dest => dest.StudentId, opt => opt.Ignore()) // B�� qua, StudentId
             .ForMember(dest => dest.IsPublished, opt => opt.Ignore()) // B�� qua, NewsFeed
             .ReverseMap();



        }
    }
}