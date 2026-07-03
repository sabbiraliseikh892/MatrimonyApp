using Dapper;
using Matrimony.Application.Features.Search;
using Matrimony.Domain.Enums;
using Matrimony.Infrastructure.Dapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Dapper.Sql
{
    public class SearchProfileSqlBuilder
    {


        public SearchSqlResult Build(
        SearchProfileRequest request,
        Guid currentUserId)
        {
            var sql = new StringBuilder();

            var whereClause = new StringBuilder();

            var parameters = new DynamicParameters();

            //-----------------------------------------------------
            // Mandatory Parameter
            //-----------------------------------------------------

            whereClause.AppendLine(@"
WHERE
up.UserId <> @CurrentUserId
");

            parameters.Add("@CurrentUserId", currentUserId);

            //-----------------------------------------------------
            // Religion
            //-----------------------------------------------------

            if (request.ReligionId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.ReligionId = @ReligionId");

                parameters.Add(
                    "@ReligionId",
                    request.ReligionId);
            }

            //-----------------------------------------------------
            // Caste
            //-----------------------------------------------------

            if (request.CasteId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.CasteId = @CasteId");

                parameters.Add(
                    "@CasteId",
                    request.CasteId);
            }

            //-----------------------------------------------------
            // Mother Tongue
            //-----------------------------------------------------

            if (request.MotherTongueId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.MotherTongueId=@MotherTongueId");

                parameters.Add(
                    "@MotherTongueId",
                    request.MotherTongueId);
            }

            //-----------------------------------------------------
            // Education
            //-----------------------------------------------------

            if (request.EducationId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.EducationId=@EducationId");

                parameters.Add(
                    "@EducationId",
                    request.EducationId);
            }

            //-----------------------------------------------------
            // Occupation
            //-----------------------------------------------------

            if (request.OccupationId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.OccupationId=@OccupationId");

                parameters.Add(
                    "@OccupationId",
                    request.OccupationId);
            }

            //-----------------------------------------------------
            // Country
            //-----------------------------------------------------

            if (request.CountryId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.CountryId=@CountryId");

                parameters.Add(
                    "@CountryId",
                    request.CountryId);
            }

            //-----------------------------------------------------
            // State
            //-----------------------------------------------------

            if (request.StateId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.StateId=@StateId");

                parameters.Add(
                    "@StateId",
                    request.StateId);
            }

            //-----------------------------------------------------
            // City
            //-----------------------------------------------------

            if (request.CityId.HasValue)
            {
                whereClause.AppendLine(
                    "AND up.CityId=@CityId");

                parameters.Add(
                    "@CityId",
                    request.CityId);
            }
            //-----------------------------------------------------
            // Age
            //-----------------------------------------------------

            if (request.MinAge.HasValue)
            {
                whereClause.AppendLine(@"
AND
(
    DATEDIFF(YEAR, up.DateOfBirth, GETDATE())
    -
    CASE
        WHEN DATEADD(
            YEAR,
            DATEDIFF(YEAR, up.DateOfBirth, GETDATE()),
            up.DateOfBirth
        ) > GETDATE()
        THEN 1
        ELSE 0
    END
) >= @MinAge");

                parameters.Add("@MinAge", request.MinAge);
            }

            if (request.MaxAge.HasValue)
            {
                whereClause.AppendLine(@"
AND
(
    DATEDIFF(YEAR, up.DateOfBirth, GETDATE())
    -
    CASE
        WHEN DATEADD(
            YEAR,
            DATEDIFF(YEAR, up.DateOfBirth, GETDATE()),
            up.DateOfBirth
        ) > GETDATE()
        THEN 1
        ELSE 0
    END
) <= @MaxAge");

                parameters.Add("@MaxAge", request.MaxAge);
            }

            //-----------------------------------------------------
            // Height
            //-----------------------------------------------------

            int? minHeight = null;

            if (request.MinHeightFeet.HasValue)
            {
                minHeight =
                    request.MinHeightFeet.Value * 12 +
                    request.MinHeightInches.GetValueOrDefault();
            }

            int? maxHeight = null;

            if (request.MaxHeightFeet.HasValue)
            {
                maxHeight =
                    request.MaxHeightFeet.Value * 12 +
                    request.MaxHeightInches.GetValueOrDefault();
            }

            if (minHeight.HasValue)
            {
                whereClause.AppendLine(@"
AND
((up.HeightFeet * 12) + up.HeightInches)
>= @MinHeight");

                parameters.Add("@MinHeight", minHeight);
            }

            if (maxHeight.HasValue)
            {
                whereClause.AppendLine(@"
AND
((up.HeightFeet * 12) + up.HeightInches)
<= @MaxHeight");

                parameters.Add("@MaxHeight", maxHeight);
            }

            //-----------------------------------------------------
            // Income
            //-----------------------------------------------------

            if (request.MinAnnualIncome.HasValue)
            {
                whereClause.AppendLine(@"
AND
up.AnnualIncome >= @MinAnnualIncome");

                parameters.Add(
                    "@MinAnnualIncome",
                    request.MinAnnualIncome);
            }

            if (request.MaxAnnualIncome.HasValue)
            {
                whereClause.AppendLine(@"
AND
up.AnnualIncome <= @MaxAnnualIncome");

                parameters.Add(
                    "@MaxAnnualIncome",
                    request.MaxAnnualIncome);
            }

            //-----------------------------------------------------
            // COUNT QUERY
            //-----------------------------------------------------

            sql.Append(@"
SELECT COUNT(*)

FROM UserProfiles up

INNER JOIN AspNetUsers u
    ON u.Id = up.UserId

INNER JOIN ReligionMasters r
    ON r.Id = up.ReligionId

INNER JOIN CasteMasters c
    ON c.Id = up.CasteId

INNER JOIN MotherTongueMasters mt
    ON mt.Id = up.MotherTongueId

INNER JOIN EducationMasters e
    ON e.Id = up.EducationId

INNER JOIN OccupationMasters o
    ON o.Id = up.OccupationId

INNER JOIN CountryMasters co
    ON co.Id = up.CountryId

INNER JOIN StateMasters s
    ON s.Id = up.StateId

INNER JOIN CityMasters ci
    ON ci.Id = up.CityId

LEFT JOIN ProfilePhotos pp
    ON pp.UserProfileId = up.Id
    AND pp.IsPrimary = 1
");

            sql.Append(whereClause);

            sql.AppendLine(";");
            //-----------------------------------------------------
            // PROFILE QUERY
            //-----------------------------------------------------

            sql.Append(@"

SELECT

    up.UserId,

    up.ProfileId,

    -- Privacy (Do not expose full name in search)
    '' AS FullName,

    -----------------------------------------------------
    -- Accurate Age
    -----------------------------------------------------

    DATEDIFF(YEAR, up.DateOfBirth, GETDATE())

    -

    CASE

        WHEN DATEADD(
            YEAR,
            DATEDIFF(YEAR, up.DateOfBirth, GETDATE()),
            up.DateOfBirth
        ) > GETDATE()

        THEN 1

        ELSE 0

    END

    AS Age,

    -----------------------------------------------------
    -- Height
    -----------------------------------------------------

    up.HeightFeet,

    up.HeightInches,

    -----------------------------------------------------
    -- Masters
    -----------------------------------------------------

    r.Name AS Religion,

    c.Name AS Caste,

    mt.Name AS MotherTongue,

    e.Name AS Education,

    o.Name AS Occupation,

    co.Name AS Country,

    s.Name AS State,

    ci.Name AS City,

    -----------------------------------------------------
    -- Income
    -----------------------------------------------------

    up.AnnualIncome,

    -----------------------------------------------------
    -- Primary Photo
    -----------------------------------------------------

    pp.PhotoUrl,

    -----------------------------------------------------
    -- Future Matching Engine
    -----------------------------------------------------

    0 AS MatchPercentage

FROM UserProfiles up

INNER JOIN AspNetUsers u
    ON u.Id = up.UserId

INNER JOIN ReligionMasters r
    ON r.Id = up.ReligionId

INNER JOIN CasteMasters c
    ON c.Id = up.CasteId

INNER JOIN MotherTongueMasters mt
    ON mt.Id = up.MotherTongueId

INNER JOIN EducationMasters e
    ON e.Id = up.EducationId

INNER JOIN OccupationMasters o
    ON o.Id = up.OccupationId

INNER JOIN CountryMasters co
    ON co.Id = up.CountryId

INNER JOIN StateMasters s
    ON s.Id = up.StateId

INNER JOIN CityMasters ci
    ON ci.Id = up.CityId

LEFT JOIN ProfilePhotos pp
    ON pp.UserProfileId = up.Id
    AND pp.IsPrimary = 1

");

            //-----------------------------------------------------
            // WHERE CLAUSE
            //-----------------------------------------------------

            sql.Append(whereClause);
            //-----------------------------------------------------
            // Sorting
            //-----------------------------------------------------

            sql.AppendLine();

            sql.Append(" ORDER BY ");

            switch (request.SortBy)
            {
                case ProfileSortBy.Age:

                    sql.Append(@"
DATEDIFF(YEAR, up.DateOfBirth, GETDATE())
-
CASE
    WHEN DATEADD(
        YEAR,
        DATEDIFF(YEAR, up.DateOfBirth, GETDATE()),
        up.DateOfBirth
    ) > GETDATE()
    THEN 1
    ELSE 0
END");

                    break;

                case ProfileSortBy.Height:

                    sql.Append("((up.HeightFeet * 12) + up.HeightInches)");

                    break;

                case ProfileSortBy.Income:

                    sql.Append("up.AnnualIncome");

                    break;

                case ProfileSortBy.Newest:

                default:

                    sql.Append("up.CreatedAt");

                    break;
            }

            //-----------------------------------------------------
            // Sort Direction
            //-----------------------------------------------------

            if (request.SortDirection == SortDirection.Asc)
            {
                sql.Append(" ASC ");
            }
            else
            {
                sql.Append(" DESC ");
            }

            //-----------------------------------------------------
            // Pagination
            //-----------------------------------------------------

            sql.Append(@"

OFFSET @Skip ROWS

FETCH NEXT @Take ROWS ONLY;

");

            parameters.Add(
                "@Skip",
                (request.PageNumber - 1) * request.PageSize);

            parameters.Add(
                "@Take",
                request.PageSize);

            //-----------------------------------------------------
            // Return
            //-----------------------------------------------------

            return new SearchSqlResult
            {
                Sql = sql.ToString(),

                Parameters = parameters
            };


        }
    }
}
