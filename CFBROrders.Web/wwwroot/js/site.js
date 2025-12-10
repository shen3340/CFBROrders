window.setTeamColor = function (color, contrastColor) {
    document.documentElement.style.setProperty('--team-color', color);
    document.documentElement.style.setProperty('--team-contrast-color', contrastColor);
};
