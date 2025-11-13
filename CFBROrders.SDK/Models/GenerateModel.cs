
using System.ComponentModel.DataAnnotations.Schema;


namespace CFBROrders.SDK.Models
{
    [Table("audit_log")]
    public partial class AuditLog
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("event")]
        public int Event { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [Column("data")]
        public string? Data { get; set; }
        [Column("cip")]
        public string? Cip { get; set; }
        [Column("ua")]
        public string? Ua { get; set; }
    }
    [Table("award_info")]
    public partial class AwardInfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("info")]
        public string? Info { get; set; }
    }
    [Table("awards")]
    public partial class Awards
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("award_id")]
        public int? AwardId { get; set; }
        [Column("award_date")]
        public DateTime? AwardDate { get; set; }
    }
    [Table("bans")]
    public partial class Bans
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("class")]
        public int? Class { get; set; }
        [Column("cip")]
        public string? Cip { get; set; }
        [Column("uname")]
        public string? Uname { get; set; }
        [Column("ua")]
        public string? Ua { get; set; }
        [Column("reason")]
        public string? Reason { get; set; }
    }
    [Table("captchas")]
    public partial class Captchas
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("creation")]
        public DateTime? Creation { get; set; }
    }
    [Table("continuation_polls")]
    public partial class ContinuationPolls
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("question")]
        public string? Question { get; set; }
        [Column("incrment")]
        public int? Incrment { get; set; }
        [Column("turn_id")]
        public int? TurnId { get; set; }
    }
    [Table("continuation_responses")]
    public partial class ContinuationResponses
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("poll_id")]
        public int? PollId { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("response")]
        public bool? Response { get; set; }
    }
    [Table("heat")]
    public partial class Heat
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("cumulative_players")]
        public long? CumulativePlayers { get; set; }
        [Column("cumulative_power")]
        public decimal? CumulativePower { get; set; }
    }
    [Table("heat_full")]
    public partial class HeatFull
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("cumulative_players")]
        public long? CumulativePlayers { get; set; }
        [Column("cumulative_power")]
        public decimal? CumulativePower { get; set; }
        [Column("owner")]
        public string? Owner { get; set; }
    }
    [Table("logs")]
    public partial class Logs
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("route")]
        public string? Route { get; set; }
        [Column("query")]
        public string? Query { get; set; }
        [Column("payload")]
        public string? Payload { get; set; }
        [Column("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
    [Table("moves")]
    public partial class Moves
    {
        [Column("season")]
        public int? Season { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("territory")]
        public int? Territory { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("team")]
        public int? Team { get; set; }
        [Column("player")]
        public int? Player { get; set; }
        [Column("mvp")]
        public bool? Mvp { get; set; }
        [Column("uname")]
        public string? Uname { get; set; }
        [Column("turns")]
        public int? Turns { get; set; }
        [Column("mvps")]
        public int? Mvps { get; set; }
        [Column("tname")]
        public string? Tname { get; set; }
        [Column("power")]
        public decimal? Power { get; set; }
        [Column("weight")]
        public decimal? Weight { get; set; }
        [Column("stars")]
        public int? Stars { get; set; }
        [Column("current_stars")]
        public int? CurrentStars { get; set; }
    }
    [Table("odds")]
    public partial class Odds
    {
        [Column("ones")]
        public int? Ones { get; set; }
        [Column("twos")]
        public int? Twos { get; set; }
        [Column("threes")]
        public int? Threes { get; set; }
        [Column("fours")]
        public int? Fours { get; set; }
        [Column("fives")]
        public int? Fives { get; set; }
        [Column("players")]
        public int? Players { get; set; }
        [Column("teampower")]
        public decimal? Teampower { get; set; }
        [Column("territorypower")]
        public decimal? Territorypower { get; set; }
        [Column("chance")]
        public decimal? Chance { get; set; }
        [Column("team")]
        public int? Team { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("territory_name")]
        public string? TerritoryName { get; set; }
        [Column("team_name")]
        public string? TeamName { get; set; }
        [Column("color")]
        public string? Color { get; set; }
        [Column("secondary_color")]
        public string? SecondaryColor { get; set; }
        [Column("tname")]
        public string? Tname { get; set; }
        [Column("prev_owner")]
        public string? PrevOwner { get; set; }
        [Column("mvp")]
        public string? Mvp { get; set; }
    }
    [Table("past_turns")]
    public partial class PastTurns
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("territory")]
        public int? Territory { get; set; }
        [Column("mvp")]
        public bool? Mvp { get; set; }
        [Column("power")]
        public decimal? Power { get; set; }
        [Column("multiplier")]
        public decimal? Multiplier { get; set; }
        [Column("weight")]
        public decimal? Weight { get; set; }
        [Column("stars")]
        public int? Stars { get; set; }
        [Column("team")]
        public int? Team { get; set; }
        [Column("alt_score")]
        public int? AltScore { get; set; }
        [Column("merc")]
        public bool? Merc { get; set; }
        [Column("turn_id")]
        public int? TurnId { get; set; }
    }
    [Table("players")]
    public partial class Players
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("uname")]
        public string? Uname { get; set; }
        [Column("platform")]
        public string? Platform { get; set; }
        [Column("current_team")]
        public int? CurrentTeam { get; set; }
        [Column("overall")]
        public int? Overall { get; set; }
        [Column("turns")]
        public int? Turns { get; set; }
        [Column("game_turns")]
        public int? GameTurns { get; set; }
        [Column("mvps")]
        public int? Mvps { get; set; }
        [Column("streak")]
        public int? Streak { get; set; }
        [Column("awards")]
        public int? Awards { get; set; }
        [Column("tname")]
        public string? Tname { get; set; }
    }
    [Table("region_ownership")]
    public partial class RegionOwnership
    {
        [Column("owner_count")]
        public long? OwnerCount { get; set; }
        [Column("owners")]
        public string? Owners { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("region")]
        public int? Region { get; set; }
    }
    [Table("regions")]
    public partial class Regions
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("submap")]
        public int Submap { get; set; }
    }
    [Table("rollinfo")]
    public partial class Rollinfo
    {
        [Column("rollstarttime")]
        public string? Rollstarttime { get; set; }
        [Column("rollendtime")]
        public string? Rollendtime { get; set; }
        [Column("chaosrerolls")]
        public int? Chaosrerolls { get; set; }
        [Column("chaosweight")]
        public int? Chaosweight { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("json_agg")]
        public string? JsonAgg { get; set; }
    }
    [Table("statistics")]
    public partial class Statistics
    {
        [Column("turn_id")]
        public int? TurnId { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("team")]
        public int? Team { get; set; }
        [Column("rank")]
        public int? Rank { get; set; }
        [Column("territorycount")]
        public int? Territorycount { get; set; }
        [Column("playercount")]
        public int? Playercount { get; set; }
        [Column("merccount")]
        public int? Merccount { get; set; }
        [Column("starpower")]
        public decimal? Starpower { get; set; }
        [Column("efficiency")]
        public decimal? Efficiency { get; set; }
        [Column("effectivepower")]
        public decimal? Effectivepower { get; set; }
        [Column("ones")]
        public int? Ones { get; set; }
        [Column("twos")]
        public int? Twos { get; set; }
        [Column("threes")]
        public int? Threes { get; set; }
        [Column("fours")]
        public int? Fours { get; set; }
        [Column("fives")]
        public int? Fives { get; set; }
        [Column("tname")]
        public string? Tname { get; set; }
        [Column("logo")]
        public string? Logo { get; set; }
        [Column("regions")]
        public long? Regions { get; set; }
    }
    [Table("stats")]
    public partial class Stats
    {
        [Column("team")]
        public int? Team { get; set; }
        [Column("rank")]
        public int? Rank { get; set; }
        [Column("territorycount")]
        public int? Territorycount { get; set; }
        [Column("playercount")]
        public int? Playercount { get; set; }
        [Column("merccount")]
        public int? Merccount { get; set; }
        [Column("starpower")]
        public decimal? Starpower { get; set; }
        [Column("efficiency")]
        public decimal? Efficiency { get; set; }
        [Column("effectivepower")]
        public decimal? Effectivepower { get; set; }
        [Column("ones")]
        public int? Ones { get; set; }
        [Column("twos")]
        public int? Twos { get; set; }
        [Column("threes")]
        public int? Threes { get; set; }
        [Column("fours")]
        public int? Fours { get; set; }
        [Column("fives")]
        public int? Fives { get; set; }
        [Column("turn_id")]
        public int? TurnId { get; set; }
    }
    [Table("team_player_moves")]
    public partial class TeamPlayerMoves
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("team")]
        public string? Team { get; set; }
        [Column("player")]
        public string? Player { get; set; }
        [Column("stars")]
        public int? Stars { get; set; }
        [Column("mvp")]
        public bool? Mvp { get; set; }
        [Column("territory")]
        public string? Territory { get; set; }
        [Column("regularteam")]
        public string? Regularteam { get; set; }
        [Column("weight")]
        public decimal? Weight { get; set; }
        [Column("power")]
        public decimal? Power { get; set; }
        [Column("multiplier")]
        public decimal? Multiplier { get; set; }
    }
    [Table("teams")]
    public partial class Teams
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("tname")]
        public string? Tname { get; set; }
        [Column("tshortname")]
        public string? Tshortname { get; set; }
        [Column("creation_date")]
        public DateTime? CreationDate { get; set; }
        [Column("color_1")]
        public string? Color1 { get; set; }
        [Column("color_2")]
        public string? Color2 { get; set; }
        [Column("logo")]
        public string? Logo { get; set; }
        [Column("seasons")]
        public string? Seasons { get; set; }
        [Column("respawn_count")]
        public int RespawnCount { get; set; }
    }
    [Table("territories")]
    public partial class Territories
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("region")]
        public int? Region { get; set; }
    }
    [Table("territory_adjacency")]
    public partial class TerritoryAdjacency
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("territory_id")]
        public int? TerritoryId { get; set; }
        [Column("adjacent_id")]
        public int? AdjacentId { get; set; }
        [Column("note")]
        public string? Note { get; set; }
        [Column("min_turn")]
        public int? MinTurn { get; set; }
        [Column("max_turn")]
        public int? MaxTurn { get; set; }
    }
    [Table("territory_neighbor_history")]
    public partial class TerritoryNeighborHistory
    {
        [Column("turn_id")]
        public int? TurnId { get; set; }
        [Column("id")]
        public int? Id { get; set; }
        [Column("neighbors")]
        public string? Neighbors { get; set; }
    }
    [Table("territory_ownership")]
    public partial class TerritoryOwnership
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("territory_id")]
        public int? TerritoryId { get; set; }
        [Column("owner_id")]
        public int? OwnerId { get; set; }
        [Column("previous_owner_id")]
        public int? PreviousOwnerId { get; set; }
        [Column("random_number")]
        public decimal? RandomNumber { get; set; }
        [Column("timestamp")]
        public DateTime? Timestamp { get; set; }
        [Column("mvp")]
        public int? Mvp { get; set; }
        [Column("turn_id")]
        public int? TurnId { get; set; }
        [Column("is_respawn")]
        public bool IsRespawn { get; set; }
    }
    [Table("territory_ownership_with_neighbors")]
    public partial class TerritoryOwnershipWithNeighbors
    {
        [Column("territory_id")]
        public int? TerritoryId { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("tname")]
        public string? Tname { get; set; }
        [Column("region")]
        public int? Region { get; set; }
        [Column("region_name")]
        public string? RegionName { get; set; }
        [Column("neighbors")]
        public string? Neighbors { get; set; }
    }
    [Table("territory_ownership_without_neighbors")]
    public partial class TerritoryOwnershipWithoutNeighbors
    {
        [Column("territory_id")]
        public int? TerritoryId { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("owner")]
        public string? Owner { get; set; }
        [Column("prev_owner")]
        public string? PrevOwner { get; set; }
        [Column("timestamp")]
        public DateTime? Timestamp { get; set; }
        [Column("random_number")]
        public decimal? RandomNumber { get; set; }
        [Column("mvp")]
        public string? Mvp { get; set; }
    }
    [Table("territory_stats")]
    public partial class TerritoryStats
    {
        [Column("team")]
        public int? Team { get; set; }
        [Column("ones")]
        public int? Ones { get; set; }
        [Column("twos")]
        public int? Twos { get; set; }
        [Column("threes")]
        public int? Threes { get; set; }
        [Column("fours")]
        public int? Fours { get; set; }
        [Column("fives")]
        public int? Fives { get; set; }
        [Column("teampower")]
        public decimal? Teampower { get; set; }
        [Column("chance")]
        public decimal? Chance { get; set; }
        [Column("id")]
        public int Id { get; set; }
        [Column("territory")]
        public int? Territory { get; set; }
        [Column("territory_power")]
        public decimal? TerritoryPower { get; set; }
        [Column("turn_id")]
        public int? TurnId { get; set; }
    }
    [Table("turninfo")]
    public partial class Turninfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("season")]
        public int? Season { get; set; }
        [Column("day")]
        public int? Day { get; set; }
        [Column("complete")]
        public bool? Complete { get; set; }
        [Column("active")]
        public bool? Active { get; set; }
        [Column("finale")]
        public bool? Finale { get; set; }
        [Column("chaosrerolls")]
        public int? Chaosrerolls { get; set; }
        [Column("chaosweight")]
        public int? Chaosweight { get; set; }
        [Column("rollendtime")]
        public DateTime? Rollendtime { get; set; }
        [Column("rollstarttime")]
        public DateTime? Rollstarttime { get; set; }
        [Column("allornothingenabled")]
        public bool? Allornothingenabled { get; set; }
        [Column("map")]
        public string? Map { get; set; }
    }
    [Table("turns")]
    public partial class Turns
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("territory")]
        public int? Territory { get; set; }
        [Column("mvp")]
        public bool Mvp { get; set; }
        [Column("power")]
        public decimal? Power { get; set; }
        [Column("multiplier")]
        public decimal? Multiplier { get; set; }
        [Column("weight")]
        public decimal? Weight { get; set; }
        [Column("stars")]
        public int? Stars { get; set; }
        [Column("team")]
        public int? Team { get; set; }
        [Column("alt_score")]
        public int? AltScore { get; set; }
        [Column("merc")]
        public bool? Merc { get; set; }
        [Column("turn_id")]
        public int? TurnId { get; set; }
    }
    [Table("users")]
    public partial class Users
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("uname")]
        public string Uname { get; set; }
        [Column("platform")]
        public string Platform { get; set; }
        [Column("join_date")]
        public DateTime? JoinDate { get; set; }
        [Column("current_team")]
        public int CurrentTeam { get; set; }
        [Column("auth_key")]
        public string? AuthKey { get; set; }
        [Column("overall")]
        public int? Overall { get; set; }
        [Column("turns")]
        public int? Turns { get; set; }
        [Column("game_turns")]
        public int? GameTurns { get; set; }
        [Column("mvps")]
        public int? Mvps { get; set; }
        [Column("streak")]
        public int? Streak { get; set; }
        [Column("awards")]
        public int? Awards { get; set; }
        [Column("role_id")]
        public int? RoleId { get; set; }
        [Column("playing_for")]
        public int? PlayingFor { get; set; }
        [Column("past_teams")]
        public string? PastTeams { get; set; }
        [Column("awards_bak")]
        public int? AwardsBak { get; set; }
        [Column("discord_id")]
        public long? DiscordId { get; set; }
        [Column("is_alt")]
        public bool? IsAlt { get; set; }
        [Column("must_captcha")]
        public bool? MustCaptcha { get; set; }
    }
}

