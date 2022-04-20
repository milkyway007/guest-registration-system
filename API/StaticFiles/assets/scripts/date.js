Date.prototype.toRender = function () {
    const yyyy = this.getFullYear();
    const mm = this.getMonth();
    const dd = this.getDate();
  
    const hh = padTo2Digits(this.getHours());
    const MM = padTo2Digits(this.getMinutes());
  
    const result = `${dd}. ${monthNames[mm]} ${yyyy} ${hh}:${MM}`;
  
    return result;
  };
  
  Date.prototype.toPost = function () {
      console.log(this)
      console.log(typeof this)
    const yyyy = this.getFullYear();
    const mm = padTo2Digits(this.getMonth() + 1);
    const dd = padTo2Digits(this.getDate());
  
    const hh = padTo2Digits(this.getHours());
    const MM = padTo2Digits(this.getMinutes());
    const ss = padTo2Digits(this.getSeconds());
  
    const result = `${dd}-${mm}-${yyyy} ${hh}:${MM}:${ss}`;
  
    return result;
  };