using MyBGList.Models;

namespace MyBGList.GraphQL
{
    public class Query
    {
        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BoardGame> GetBoardGames(
            [Service] ApplicationDbContext context)
            => context.BoardGames;

        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Domain> GetDomains(
            [Service] ApplicationDbContext context)
            => context.Domains;

        [Serial]
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Mechanic> GetMechanics(
            [Service] ApplicationDbContext context)
            => context.Mechanics;
    }
}
