using Dongi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dongi.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options )
        {

        }

        public DbSet<Person> Persons => Set<Person>( );
        public DbSet<Event> Events => Set<Event>( );
        public DbSet<EventPerson> EventPersons => Set<EventPerson>( );
        public DbSet<Expense> Expenses => Set<Expense>( );
        public DbSet<ExpensePerson> ExpensePersons => Set<ExpensePerson>( );

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );

            ConfigurePerson( builder );
            ConfigureEvent( builder );
            ConfigureEventPerson( builder );
            ConfigureExpense( builder );
            ConfigureExpensePerson( builder );
        }

        private static void ConfigurePerson( ModelBuilder builder )
        {
            builder.Entity<Person>( )
                .HasIndex( p => p.UserId )
                .IsUnique( );

            builder.Entity<Person>( )
                .HasOne( p => p.User )
                .WithMany( )
                .HasForeignKey( p => p.UserId )
                .OnDelete( DeleteBehavior.Restrict );
        }

        private static void ConfigureEvent( ModelBuilder builder )
        {
            builder.Entity<Event>( )
                .HasOne( e => e.CreatedBy )
                .WithMany( )
                .HasForeignKey( e => e.CreatedByPersonId )
                .OnDelete( DeleteBehavior.Restrict );
        }

        private static void ConfigureEventPerson( ModelBuilder builder )
        {
            builder.Entity<EventPerson>( )
                .HasIndex( ep => new { ep.EventId, ep.PersonId } )
                .IsUnique( );

            builder.Entity<EventPerson>( )
                .HasOne( ep => ep.Event )
                .WithMany( e => e.EventPersons )
                .HasForeignKey( ep => ep.EventId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Entity<EventPerson>( )
                .HasOne( ep => ep.Person )
                .WithMany( p => p.EventPersons )
                .HasForeignKey( ep => ep.PersonId )
                .OnDelete( DeleteBehavior.Restrict );
        }

        private static void ConfigureExpense( ModelBuilder builder )
        {
            builder.Entity<Expense>( )
                .HasOne( e => e.Event )
                .WithMany( ev => ev.Expenses )
                .HasForeignKey( e => e.EventId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Entity<Expense>( )
                .HasOne( e => e.PaidBy )
                .WithMany( p => p.ExpensesPaid )
                .HasForeignKey( e => e.PaidByPersonId )
                .OnDelete( DeleteBehavior.Restrict );
        }

        private static void ConfigureExpensePerson( ModelBuilder builder )
        {
            builder.Entity<ExpensePerson>( )
                .HasIndex( ep => new { ep.ExpenseId, ep.PersonId } )
                .IsUnique( );

            builder.Entity<ExpensePerson>( )
                .HasOne( ep => ep.Expense )
                .WithMany( e => e.ExpensePersons )
                .HasForeignKey( ep => ep.ExpenseId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.Entity<ExpensePerson>( )
                .HasOne( ep => ep.Person )
                .WithMany( p => p.ExpensePersons )
                .HasForeignKey( ep => ep.PersonId )
                .OnDelete( DeleteBehavior.Restrict );
        }
    }
}
