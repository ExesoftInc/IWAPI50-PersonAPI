using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Entities {
    
    
    public static class ModelExtender {
        
        public static Address ToEntity(this AddressModel model) {

            Address entity = new Address();
            entity.AddressID = model.AddressID;
            entity.AddressLine1 = model.AddressLine1;
            entity.AddressLine2 = model.AddressLine2;
            entity.City = model.City;
            entity.StateProvinceID = model.StateProvinceID;
            entity.PostalCode = model.PostalCode;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static Address ToEntity(this AddressModel model, Address entity) {

            entity.AddressLine1 = model.AddressLine1;
            entity.AddressLine2 = model.AddressLine2;
            entity.City = model.City;
            entity.StateProvinceID = model.StateProvinceID;
            entity.PostalCode = model.PostalCode;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static AddressType ToEntity(this AddressTypeModel model) {

            AddressType entity = new AddressType();
            entity.AddressTypeID = model.AddressTypeID;
            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static AddressType ToEntity(this AddressTypeModel model, AddressType entity) {

            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static BusinessEntity ToEntity(this BusinessEntityModel model) {

            BusinessEntity entity = new BusinessEntity();
            entity.BusinessEntityID = model.BusinessEntityID;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static BusinessEntity ToEntity(this BusinessEntityModel model, BusinessEntity entity) {

            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static BusinessEntityAddress ToEntity(this BusinessEntityAddressModel model) {

            BusinessEntityAddress entity = new BusinessEntityAddress();
            entity.BusinessEntityID = model.BusinessEntityID;
            entity.AddressID = model.AddressID;
            entity.AddressTypeID = model.AddressTypeID;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static BusinessEntityAddress ToEntity(this BusinessEntityAddressModel model, BusinessEntityAddress entity) {

            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static BusinessEntityContact ToEntity(this BusinessEntityContactModel model) {

            BusinessEntityContact entity = new BusinessEntityContact();
            entity.BusinessEntityID = model.BusinessEntityID;
            entity.PersonID = model.PersonID;
            entity.ContactTypeID = model.ContactTypeID;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static BusinessEntityContact ToEntity(this BusinessEntityContactModel model, BusinessEntityContact entity) {

            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static ContactType ToEntity(this ContactTypeModel model) {

            ContactType entity = new ContactType();
            entity.ContactTypeID = model.ContactTypeID;
            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static ContactType ToEntity(this ContactTypeModel model, ContactType entity) {

            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static CountryRegion ToEntity(this CountryRegionModel model) {

            CountryRegion entity = new CountryRegion();
            entity.CountryRegionCode = model.CountryRegionCode;
            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static CountryRegion ToEntity(this CountryRegionModel model, CountryRegion entity) {

            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static EmailAddress ToEntity(this EmailAddressModel model) {

            EmailAddress entity = new EmailAddress();
            entity.BusinessEntityID = model.BusinessEntityID;
            entity.EmailAddressID = model.EmailAddressID;
            entity.EmailAddress_ = model.EmailAddress_;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static EmailAddress ToEntity(this EmailAddressModel model, EmailAddress entity) {

            entity.EmailAddress_ = model.EmailAddress_;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static Password ToEntity(this PasswordModel model) {

            Password entity = new Password();
            entity.BusinessEntityID = model.BusinessEntityID;
            entity.PasswordHash = model.PasswordHash;
            entity.PasswordSalt = model.PasswordSalt;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static Password ToEntity(this PasswordModel model, Password entity) {

            entity.PasswordHash = model.PasswordHash;
            entity.PasswordSalt = model.PasswordSalt;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static Person ToEntity(this PersonModel model) {

            Person entity = new Person();
            entity.BusinessEntityID = model.BusinessEntityID;
            entity.PersonType = model.PersonType;
            entity.NameStyle = model.NameStyle;
            entity.Title = model.Title;
            entity.FirstName = model.FirstName;
            entity.MiddleName = model.MiddleName;
            entity.LastName = model.LastName;
            entity.Suffix = model.Suffix;
            entity.EmailPromotion = model.EmailPromotion;
            entity.AdditionalContactInfo = model.AdditionalContactInfo;
            entity.Demographics = model.Demographics;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static Person ToEntity(this PersonModel model, Person entity) {

            entity.PersonType = model.PersonType;
            entity.NameStyle = model.NameStyle;
            entity.Title = model.Title;
            entity.FirstName = model.FirstName;
            entity.MiddleName = model.MiddleName;
            entity.LastName = model.LastName;
            entity.Suffix = model.Suffix;
            entity.EmailPromotion = model.EmailPromotion;
            entity.AdditionalContactInfo = model.AdditionalContactInfo;
            entity.Demographics = model.Demographics;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static PersonPhone ToEntity(this PersonPhoneModel model) {

            PersonPhone entity = new PersonPhone();
            entity.BusinessEntityID = model.BusinessEntityID;
            entity.PhoneNumber = model.PhoneNumber;
            entity.PhoneNumberTypeID = model.PhoneNumberTypeID;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static PersonPhone ToEntity(this PersonPhoneModel model, PersonPhone entity) {

            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static PhoneNumberType ToEntity(this PhoneNumberTypeModel model) {

            PhoneNumberType entity = new PhoneNumberType();
            entity.PhoneNumberTypeID = model.PhoneNumberTypeID;
            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static PhoneNumberType ToEntity(this PhoneNumberTypeModel model, PhoneNumberType entity) {

            entity.Name = model.Name;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static StateProvince ToEntity(this StateProvinceModel model) {

            StateProvince entity = new StateProvince();
            entity.StateProvinceID = model.StateProvinceID;
            entity.StateProvinceCode = model.StateProvinceCode;
            entity.CountryRegionCode = model.CountryRegionCode;
            entity.IsOnlyStateProvinceFlag = model.IsOnlyStateProvinceFlag;
            entity.Name = model.Name;
            entity.TerritoryID = model.TerritoryID;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
        
        public static StateProvince ToEntity(this StateProvinceModel model, StateProvince entity) {

            entity.StateProvinceCode = model.StateProvinceCode;
            entity.CountryRegionCode = model.CountryRegionCode;
            entity.IsOnlyStateProvinceFlag = model.IsOnlyStateProvinceFlag;
            entity.Name = model.Name;
            entity.TerritoryID = model.TerritoryID;
            entity.ModifiedDate = model.ModifiedDate;

            return entity;
        }
    }
}

