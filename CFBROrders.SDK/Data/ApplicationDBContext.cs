using System;
using System.Collections.Generic;
using CFBROrders.SDK.Models;
using Microsoft.EntityFrameworkCore;

namespace CFBROrders.SDK.Data;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<AwardInfo> AwardInfos { get; set; }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<Captcha> Captchas { get; set; }

    public virtual DbSet<ContinuationPoll> ContinuationPolls { get; set; }

    public virtual DbSet<ContinuationResponse> ContinuationResponses { get; set; }

    public virtual DbSet<Heat> Heats { get; set; }

    public virtual DbSet<HeatFull> HeatFulls { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Move> Moves { get; set; }

    public virtual DbSet<Odd> Odds { get; set; }

    public virtual DbSet<PastTurn> PastTurns { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RegionOwnership> RegionOwnerships { get; set; }

    public virtual DbSet<Rollinfo> Rollinfos { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamPlayerMove> TeamPlayerMoves { get; set; }

    public virtual DbSet<TerritoryAdjacency> TerritoryAdjacencies { get; set; }

    public virtual DbSet<TerritoryNeighborHistory> TerritoryNeighborHistories { get; set; }

    public virtual DbSet<TerritoryOwnership> TerritoryOwnerships { get; set; }

    public virtual DbSet<TerritoryOwnershipWithNeighbor> TerritoryOwnershipWithNeighbors { get; set; }

    public virtual DbSet<TerritoryOwnershipWithoutNeighbor> TerritoryOwnershipWithoutNeighbors { get; set; }

    public virtual DbSet<TerritoryStat> TerritoryStats { get; set; }

    public virtual DbSet<Turn> Turns { get; set; }

    public virtual DbSet<Turninfo> Turninfos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("rr_event", new[] { "notification", "change_team" })
            .HasPostgresExtension("citext")
            .HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Award>(entity =>
        {
            entity.Property(e => e.AwardDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<AwardInfo>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Ban>(entity =>
        {
            entity.Property(e => e.Class).HasComment("// Username: 1\n// Prevent ban, username, for suspend flag: 2\n// Allow login without email: 3\n// Prevent ban, Reddit ban: 4");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Reason).UseCollation("en_US.utf8");
        });

        modelBuilder.Entity<Captcha>(entity =>
        {
            entity.Property(e => e.Creation).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<ContinuationPoll>(entity =>
        {
            entity.Property(e => e.Incrment).HasDefaultValue(7);
            entity.Property(e => e.Question).HasDefaultValueSql("'Should this season be extended by seven more days?'::text");
        });

        modelBuilder.Entity<Heat>(entity =>
        {
            entity.ToView("heat");
        });

        modelBuilder.Entity<HeatFull>(entity =>
        {
            entity.ToView("heat_full");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Move>(entity =>
        {
            entity.ToView("moves");
        });

        modelBuilder.Entity<Odd>(entity =>
        {
            entity.ToView("odds");
        });

        modelBuilder.Entity<PastTurn>(entity =>
        {
            entity.ToView("past_turns");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToView("players");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.Submap).HasDefaultValue(0);
        });

        modelBuilder.Entity<RegionOwnership>(entity =>
        {
            entity.ToView("region_ownership");
        });

        modelBuilder.Entity<Rollinfo>(entity =>
        {
            entity.ToView("rollinfo");
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.ToView("statistics");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.RespawnCount).HasDefaultValue(0);
        });

        modelBuilder.Entity<TeamPlayerMove>(entity =>
        {
            entity.ToView("team_player_moves");
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("nextval('Teams_seq'::regclass)");
        });

        modelBuilder.Entity<TerritoryAdjacency>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TerritoryNeighborHistory>(entity =>
        {
            entity.ToView("territory_neighbor_history");
        });

        modelBuilder.Entity<TerritoryOwnership>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsRespawn).HasDefaultValue(false);
            entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<TerritoryOwnershipWithNeighbor>(entity =>
        {
            entity.ToView("territory_ownership_with_neighbors");
        });

        modelBuilder.Entity<TerritoryOwnershipWithoutNeighbor>(entity =>
        {
            entity.ToView("territory_ownership_without_neighbors");
        });

        modelBuilder.Entity<TerritoryStat>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("nextval('audit_log_id_seq'::regclass)");
        });

        modelBuilder.Entity<Turn>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Merc).HasDefaultValue(false);
            entity.Property(e => e.Mvp).HasDefaultValue(false);
        });

        modelBuilder.Entity<Turninfo>(entity =>
        {
            entity.Property(e => e.Chaosrerolls).HasDefaultValue(0);
            entity.Property(e => e.Chaosweight).HasDefaultValue(1);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Awards).HasDefaultValue(0);
            entity.Property(e => e.CurrentTeam).HasDefaultValueSql("'-1'::integer");
            entity.Property(e => e.GameTurns).HasDefaultValue(0);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsAlt).HasDefaultValue(false);
            entity.Property(e => e.JoinDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.MustCaptcha).HasDefaultValue(true);
            entity.Property(e => e.Mvps).HasDefaultValue(0);
            entity.Property(e => e.Overall).HasDefaultValue(1);
            entity.Property(e => e.PlayingFor).HasDefaultValueSql("'-1'::integer");
            entity.Property(e => e.RoleId).HasDefaultValue(0);
            entity.Property(e => e.Streak).HasDefaultValue(0);
            entity.Property(e => e.Turns).HasDefaultValue(0);
        });
        modelBuilder.HasSequence<int>("audit_log_id_seq");
        modelBuilder.HasSequence<int>("award_info_id_seq");
        modelBuilder.HasSequence<int>("awards_id_seq");
        modelBuilder.HasSequence<int>("bans_id_seq");
        modelBuilder.HasSequence<int>("captchas_id_seq");
        modelBuilder.HasSequence<int>("continuation_polls_id_seq");
        modelBuilder.HasSequence<int>("continuation_responses_id_seq");
        modelBuilder.HasSequence<int>("logs_id_seq");
        modelBuilder.HasSequence<int>("regions_id_seq");
        modelBuilder.HasSequence<int>("teams_id_seq");
        modelBuilder.HasSequence("Teams_seq");
        modelBuilder.HasSequence<int>("territory_adjacency_id_seq");
        modelBuilder.HasSequence<int>("territory_ownership_id_seq");
        modelBuilder.HasSequence<int>("territory_stats_id_seq");
        modelBuilder.HasSequence<int>("turninfo_id_seq");
        modelBuilder.HasSequence<int>("turns_id_seq");
        modelBuilder.HasSequence<int>("users_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
