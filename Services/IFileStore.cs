using System;

namespace RealEstateCore.Services;


public interface IFileStore{
    string Save(IFormFile file,string location,string name);
}