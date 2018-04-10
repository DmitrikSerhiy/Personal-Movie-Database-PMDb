using PMDb.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace PMDb.Infrastructure.Data
{
    public class DuplicateChecker
    {
        private MovieContext context;
        public DuplicateChecker(MovieContext Context)
        {
            context = Context;
        }
        //that's redundant but it's too minor feature to write it via Dynamiv Linq, so let it be as it is
        public void CheckAndInitActors(IList<MovieActor> MovieActor)
        {
            var actorsName = MovieActor.Select(a => a.Actor.Name).ToArray();

            var actorsNameToinit = context.Actors
                .Where(u => actorsName.Contains(u.Name)).Select(a => a.Name).Distinct();

            var actorsToInit = MovieActor.Select(a => a.Actor)
                .Where(aa => actorsNameToinit.Contains(aa.Name)).ToArray();

            for (int i = 0; i < actorsToInit.Count(); i++)
            {
                var tempActor = context.Actors.FirstOrDefault(a => a.Name == actorsToInit[i].Name);
                MovieActor.FirstOrDefault(a => a.Actor.Name == tempActor.Name).Actor = tempActor;
                context.Actors.Attach(tempActor);
            }
        }

        public void CheckAndInitDirectors(IList<MovieDirector> MovieDirector)
        {
            var directorsName = MovieDirector.Select(a => a.Director.Name).ToArray();

            var directorsNameToinit = context.Directors
                .Where(u => directorsName.Contains(u.Name)).Select(a => a.Name).Distinct();

            var directorsToInit = MovieDirector.Select(a => a.Director)
                .Where(aa => directorsNameToinit.Contains(aa.Name)).ToArray();

            for (int i = 0; i < directorsToInit.Count(); i++)
            {
                var tempDirector = context.Directors.FirstOrDefault(a => a.Name == directorsToInit[i].Name);
                MovieDirector.FirstOrDefault(a => a.Director.Name == tempDirector.Name).Director = tempDirector;
                context.Directors.Attach(tempDirector);
            }
        }

        public void CheckAndInitGenres(IList<MovieGenre> MovieGenre)
        {
            var genresName = MovieGenre.Select(a => a.Genre.Name).ToArray();

            var genresNameToinit = context.Genres
                .Where(u => genresName.Contains(u.Name)).Select(a => a.Name).Distinct();

            var genresToInit = MovieGenre.Select(a => a.Genre)
                .Where(aa => genresNameToinit.Contains(aa.Name)).ToArray();

            for (int i = 0; i < genresToInit.Count(); i++)
            {
                var tempGenre = context.Genres.FirstOrDefault(a => a.Name == genresToInit[i].Name);
                MovieGenre.FirstOrDefault(a => a.Genre.Name == tempGenre.Name).Genre = tempGenre;
                context.Genres.Attach(tempGenre);
            }
        }

        public void CheckAndInitTags(IList<MovieTag> MovieTag)
        {
            var tagsName = MovieTag.Select(a => a.Tag.Name).ToArray();

            var tagsNameToinit = context.Tags
                .Where(u => tagsName.Contains(u.Name)).Select(a => a.Name).Distinct();

            var tagsToInit = MovieTag.Select(a => a.Tag)
                .Where(aa => tagsNameToinit.Contains(aa.Name)).ToArray();

            for (int i = 0; i < tagsToInit.Count(); i++)
            {
                var tempTag = context.Tags.FirstOrDefault(a => a.Name == tagsToInit[i].Name);
                MovieTag.FirstOrDefault(a => a.Tag.Name == tempTag.Name).Tag = tempTag;
                context.Tags.Attach(tempTag);
            }
        }

        public void CheckAndInitWriters(IList<MovieWriter> MovieWriter)
        {
            var writersName = MovieWriter.Select(a => a.Writer.Name).ToArray();

            var writersNameToinit = context.Writers
                .Where(u => writersName.Contains(u.Name)).Select(a => a.Name).Distinct();

            var writersToInit = MovieWriter.Select(a => a.Writer)
                .Where(aa => writersNameToinit.Contains(aa.Name)).ToArray();

            for (int i = 0; i < writersToInit.Count(); i++)
            {
                var tempWriter = context.Writers.FirstOrDefault(a => a.Name == writersToInit[i].Name);
                MovieWriter.FirstOrDefault(a => a.Writer.Name == tempWriter.Name).Writer = tempWriter;
                context.Writers.Attach(tempWriter);
            }
        }

    }
}
