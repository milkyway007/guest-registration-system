const monthNames = ["jaanuar", "veebruar", "märts", "aprill", "mai", "juuni",
  "juuli", "august", "september", "oktoober", "november", "detsember"
];

Date.prototype.toHumanReadableDateTime = function() {
    const yyyy = this.getFullYear();
    const mm = this.getMonth(); // getMonth() is zero-based
    const dd = this.getDate();
  
    const hh = this.getHours();
    const MM = this.getMinutes();
  
    const result = `${dd}. ${monthNames[mm]} ${yyyy} ${hh}:${MM}`;
  
    return result;
  };