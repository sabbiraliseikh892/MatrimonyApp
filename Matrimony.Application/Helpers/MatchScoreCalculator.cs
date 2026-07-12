using Matrimony.Application.Features.PartnerPreference;
using Matrimony.Application.Features.Profile.CreateProfile;
using Matrimony.Application.Features.Recommendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Application.Helpers
{
    public static class MatchScoreCalculator
    {
        public static int Calculate(
            RecommendationResponse candidate,
            ProfileResponse currentUser,
            PartnerPreferenceResponse? preference)
        {
            int score = 0;

            //---------------------------------------------------
            // If no partner preference exists,
            // use basic comparison.
            //---------------------------------------------------

            if (preference == null)
            {
                return CalculateWithoutPreference(candidate, currentUser);
            }

            //---------------------------------------------------
            // Age (15)
            //---------------------------------------------------

            score += AgeScoreHelper.Calculate(
                candidate.Age,
                preference.MinAge,
                preference.MaxAge);

            //---------------------------------------------------
            // Height (10)
            //---------------------------------------------------

            score += HeightScoreHelper.Calculate(
                candidate.HeightFeet,
                candidate.HeightInches,
                preference.MinHeightFeet,
                preference.MinHeightInches,
                preference.MaxHeightFeet,
                preference.MaxHeightInches);

            //---------------------------------------------------
            // Income (5)
            //---------------------------------------------------

            score += IncomeScoreHelper.Calculate(
                candidate.AnnualIncome,
                preference.MinAnnualIncome,
                preference.MaxAnnualIncome);

            //---------------------------------------------------
            // Education (10)
            //---------------------------------------------------

            score += EducationScoreHelper.Calculate(
                currentUser.Education,
                candidate.Education);

            //---------------------------------------------------
            // Occupation (10)
            //---------------------------------------------------

            score += OccupationScoreHelper.Calculate(
                currentUser.Occupation,
                candidate.Occupation);

            //---------------------------------------------------
            // Location (5)
            //---------------------------------------------------

            score += LocationScoreHelper.Calculate(
                currentUser.CityId,
                currentUser.StateId,
                currentUser.CountryId,
                candidate.CityId,
                candidate.StateId,
                candidate.CountryId);

            //---------------------------------------------------
            // Profile Completeness (5)
            //---------------------------------------------------

            score += ProfileCompletenessHelper.Calculate(
                candidate.AboutMe,
                candidate.PrimaryPhotoUrl,
                candidate.Education,
                candidate.Occupation,
                candidate.AnnualIncome,
                candidate.CityId);

            //---------------------------------------------------
            // Favorite Bonus (2)
            //---------------------------------------------------

            if (candidate.IsFavorite)
                score += 2;

            //---------------------------------------------------
            // Recently Viewed Bonus (2)
            //---------------------------------------------------

            if (candidate.RecentlyViewed)
                score += 2;

            //---------------------------------------------------
            // Interest Sent Bonus (1)
            //---------------------------------------------------

            if (candidate.InterestSent)
                score += 1;

            return Math.Min(score, 100);
        }

        private static int CalculateWithoutPreference(
            RecommendationResponse candidate,
            ProfileResponse currentUser)
        {
            int score = 0;

            score += EducationScoreHelper.Calculate(
                currentUser.Education,
                candidate.Education);

            score += OccupationScoreHelper.Calculate(
                currentUser.Occupation,
                candidate.Occupation);

            score += LocationScoreHelper.Calculate(
                currentUser.CityId,
                currentUser.StateId,
                currentUser.CountryId,
                candidate.CityId,
                candidate.StateId,
                candidate.CountryId);

            score += ProfileCompletenessHelper.Calculate(
                candidate.AboutMe,
                candidate.PrimaryPhotoUrl,
                candidate.Education,
                candidate.Occupation,
                candidate.AnnualIncome,
                candidate.CityId);

            if (candidate.IsFavorite)
                score += 2;

            if (candidate.RecentlyViewed)
                score += 2;

            return Math.Min(score, 100);
        }
    }
}
