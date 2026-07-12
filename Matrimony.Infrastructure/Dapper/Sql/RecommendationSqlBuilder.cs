using Dapper;
using Matrimony.Application.Features.Recommendation;
using Matrimony.Infrastructure.Dapper.Models;
using System.Text;

namespace Matrimony.Infrastructure.Dapper.Sql
{
    public class RecommendationSqlBuilder
    {
        public RecommendationSqlResult Build(
            Guid currentUserId,
            RecommendationRequest request)
        {
            var sql = new StringBuilder();

            var parameters = new DynamicParameters();

            parameters.Add("CurrentUserId", currentUserId);
            parameters.Add("Offset",
                (request.PageNumber - 1) * request.PageSize);
            parameters.Add("PageSize", request.PageSize);

            sql.Append(@"

SELECT

    ------------------------------------------------------------
    -- User
    ------------------------------------------------------------
    u.Id AS UserId,

    ------------------------------------------------------------
    -- Profile
    ------------------------------------------------------------
    p.ProfileId,
    p.AboutMe,
    p.DateOfBirth,

    p.HeightFeet,
    p.HeightInches,

    p.AnnualIncome,

    p.CityId,
    p.StateId,
    p.CountryId,

    ------------------------------------------------------------
    -- Master Data
    ------------------------------------------------------------
    p.EducationId,
    e.Name AS Education,

    p.OccupationId,
    o.Name AS Occupation,

    c.Name AS City,

    ------------------------------------------------------------
    -- Primary Photo
    ------------------------------------------------------------
    photo.PhotoUrl AS PrimaryPhotoUrl,

    ------------------------------------------------------------
    -- User Information
    ------------------------------------------------------------
    CONCAT(u.FirstName,' ',u.LastName) AS FullName,

    ------------------------------------------------------------
    -- Favorite
    ------------------------------------------------------------
    CASE
        WHEN fav.Id IS NULL
            THEN CAST(0 AS bit)
        ELSE CAST(1 AS bit)
    END AS IsFavorite,

    ------------------------------------------------------------
    -- Interest Sent
    ------------------------------------------------------------
    CASE
        WHEN intr.Id IS NULL
            THEN CAST(0 AS bit)
        ELSE CAST(1 AS bit)
    END AS InterestSent,

    ------------------------------------------------------------
    -- Recently Viewed
    ------------------------------------------------------------
    CASE
        WHEN pv.Id IS NULL
            THEN CAST(0 AS bit)
        ELSE CAST(1 AS bit)
    END AS RecentlyViewed

FROM AspNetUsers u

INNER JOIN UserProfiles p
    ON p.UserId = u.Id

LEFT JOIN EducationMasters e
    ON e.Id = p.EducationId

LEFT JOIN OccupationMasters o
    ON o.Id = p.OccupationId

LEFT JOIN CityMasters c
    ON c.Id = p.CityId

------------------------------------------------------------
-- Primary Photo
------------------------------------------------------------
OUTER APPLY
(
    SELECT TOP (1)
        PhotoUrl
    FROM ProfilePhotos
    WHERE UserProfileId = p.Id
      AND IsPrimary = 1
      AND IsDeleted = 0
) photo

------------------------------------------------------------
-- Favorite
------------------------------------------------------------
LEFT JOIN UserFavorites fav
    ON fav.UserId = @CurrentUserId
   AND fav.FavoriteUserId = u.Id
   AND fav.IsDeleted = 0

------------------------------------------------------------
-- Interest
------------------------------------------------------------
LEFT JOIN UserInterests intr
    ON intr.FromUserId = @CurrentUserId
   AND intr.ToUserId = u.Id
   AND intr.IsDeleted = 0

------------------------------------------------------------
-- Recently Viewed
------------------------------------------------------------
LEFT JOIN UserProfileViews pv
    ON pv.ViewerUserId = @CurrentUserId
   AND pv.ViewedUserId = u.Id
   AND pv.IsDeleted = 0

WHERE

    ------------------------------------------------------------
    -- Don't recommend myself
    ------------------------------------------------------------
    u.Id <> @CurrentUserId

    ------------------------------------------------------------
    -- Active User
    ------------------------------------------------------------
    AND u.IsDeleted = 0

    ------------------------------------------------------------
    -- Active Profile
    ------------------------------------------------------------
    AND p.IsDeleted = 0

    ------------------------------------------------------------
    -- Exclude accepted/rejected interests
    ------------------------------------------------------------
    AND NOT EXISTS
    (
        SELECT 1
        FROM UserInterests ui
        WHERE
        (
            ui.FromUserId = @CurrentUserId
            AND ui.ToUserId = u.Id
        )
        OR
        (
            ui.FromUserId = u.Id
            AND ui.ToUserId = @CurrentUserId
        )
        AND ui.Status IN (1,2)
        AND ui.IsDeleted = 0
    )

ORDER BY
    p.CreatedAt DESC

OFFSET @Offset ROWS

FETCH NEXT @PageSize ROWS ONLY;

");

            return new RecommendationSqlResult
            {
                Sql = sql.ToString(),
                Parameters = parameters
            };
        }
    }
}