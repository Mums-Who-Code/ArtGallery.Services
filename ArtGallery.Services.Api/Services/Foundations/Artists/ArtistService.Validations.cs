// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using ArtGallery.Services.Api.Models.Artists;
using ArtGallery.Services.Api.Models.Artists.Exceptions;

namespace ArtGallery.Services.Api.Services.Foundations.Artists
{
    public partial class ArtistService
    {
        private static void ValidateInput(Artist artist)
        {
            ValidateArtistIsNotNull(artist);

            Validate(
                (Rule: IsInvalid(artist.Id), Parameter: nameof(Artist.Id)),
                (Rule: IsInvalid(text: artist.FirstName), Parameter: nameof(Artist.FirstName)),
                (Rule: IsInvalid(text: artist.LastName), Parameter: nameof(Artist.LastName)),
                (Rule: IsInvalidEmail(emailAddress: artist.Email), Parameter: nameof(Artist.Email)),
                (Rule: IsInvalidNumber(artist.ContactNumber), Parameter: nameof(Artist.ContactNumber)),
                (Rule: IsInvalid(artist.Status), Parameter: nameof(Artist.Status)),
                (Rule: IsInvalid(id: artist.CreatedBy), Parameter: nameof(Artist.CreatedBy)),
                (Rule: IsInvalid(id: artist.UpdatedBy), Parameter: nameof(Artist.UpdatedBy)),
                (Rule: IsInvalid(artist.CreatedDate), Parameter: nameof(Artist.CreatedDate)),
                (Rule: IsInvalid(artist.UpdatedDate), Parameter: nameof(Artist.UpdatedDate)));
        }

        private static void ValidateArtistIsNotNull(Artist artist)
        {
            if (artist == null)
            {
                throw new NullArtistException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Id is required"
        };

        private static dynamic IsInvalid(ArtistStatus status) => new
        {
            Condition = status != ArtistStatus.Active,
            Message = "Value is invalid."
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static bool HasNoValue(string number) =>
            String.IsNullOrWhiteSpace(number);

        private static dynamic IsInvalidNumber(string number) => new
        {
            Condition = IsInvalidContactNumber(number),
            Message = "Text is invalid."
        };

        private static bool IsInvalidContactNumber(string number)
        {
            bool isInvalid = HasNoValue(number);

            if (isInvalid is not true)
            {
                return !IsValidContactNumberFormat(number);
            }

            return isInvalid;
        }

        private static bool IsValidContactNumberFormat(string number)
        {
            return Regex.IsMatch(
                input: number,
                pattern: @"^[0-9]{10}$");
        }

        private static dynamic IsInvalidEmail(string emailAddress) => new
        {
            Condition = IsInvalidEmailFormat(emailAddress),
            Message = "Text is invalid."
        };

        private static bool IsInvalidEmailFormat(string emailAddress)
        {
            bool isInvalid = HasNoValue(emailAddress);

            if (isInvalid is not true)
            {
                return !IsValidEmailFormat(emailAddress);
            }

            return isInvalid;
        }

        private static bool IsValidEmailFormat(string emailAddress)
        {
            return Regex.IsMatch(
                input: emailAddress,
                pattern: @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                options: RegexOptions.IgnoreCase);
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidArtistException = new InvalidArtistException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidArtistException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidArtistException.ThrowIfContainsErrors();
        }
    }
}
