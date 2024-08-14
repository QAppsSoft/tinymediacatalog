﻿using Common.Extensions.Enumerable;
using Domain;
using Domain.Models.Movie;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Media;
using Services.Media.Strategy;
using Tools.IO;

namespace Services.Media;

public class MultimediaLoader(
    IDbContextFactory<MediaManagerDatabaseContext> databaseContextFactory,
    IOInterface nfoReader,
    IFileStrategy fileStrategy) : IMultimediaLoader
{
    /// <summary>
    /// Load new movie nfo in the database
    /// </summary>
    /// <param name="path">Path to the root movie folder</param>
    /// <returns>Return Guid of the recently loaded movie or <see cref="Guid.Empty"/> if movie already exist in DB</returns>
    public async Task<Guid> LoadMovieAsync(string path)
    {
        var context = await databaseContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await using (context.ConfigureAwait(false))
        {
            var exist = await context.Movies.AnyAsync(m => m.BasePath == path).ConfigureAwait(false);

            if (exist)
            {
                return Guid.Empty;
            }

            var movie = new MovieContainer { BasePath = path };

            context.Add(movie);
            await context.SaveChangesAsync().ConfigureAwait(false);

            await UpdateFilesAsync(movie, path).ConfigureAwait(false);

            await nfoReader.LoadMovieAsync(movie.Id).ConfigureAwait(false);

            return movie.Id;
        }
    }

    private async Task UpdateFilesAsync(MovieContainer movie, string path)
    {
        var filePaths = Directory.EnumerateFiles(path);

        var files = await Task.WhenAll(filePaths.Select(fileStrategy.PrepareAsync)).ConfigureAwait(false);

        movie.Files.AddRange(files);
    }

    public Task<Guid> UpdateMovieAsync(string path)
    {
        throw new NotImplementedException();
    }
}