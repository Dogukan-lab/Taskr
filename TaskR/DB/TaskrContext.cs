﻿using Microsoft.EntityFrameworkCore;

namespace TaskR.DB
{
    public class TaskrContext: DbContext
    {
        TaskrContext(DbContextOptions<TaskrContext> options): base(options)
        {}

        public DbSet<Task> Tasks { get; set; }

    }
}
