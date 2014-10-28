function init() {
    var mouseIsDown = false, last = {};

    var canvas = d3.selectAll(".cvs").each(function () {
        last[this.id] = {};
    });

    canvas.on("mousedown", function () {
        mouseIsDown = true;
        last[this.id].x = d3.mouse(this)[0];
        last[this.id].y = d3.mouse(this)[1];
    });

    canvas.on("mouseup", function () {
        mouseIsDown = false;
    });

    canvas.on("mousemove", function () {
        if (mouseIsDown) {
            var c = this.getContext('2d');

            var iX = d3.mouse(this)[0];
            var iY = d3.mouse(this)[1];
            c.moveTo(last[this.id].x, last[this.id].y);
            c.lineTo(iX, iY);
            c.stroke();
            last[this.id].x = iX;
            last[this.id].y = iY;
        }
    });

    canvas.each(function () {
            var c = this.getContext('2d');

            c.fillStyle = '#fff';
            c.fillRect(0, 0, 200, 100);
            c.fillStyle = 'red';
            c.fillRect(0, 0, 5, 5);
            c.fillRect(195, 95, 5, 5);
            c.fillRect(0, 95, 5, 5);
            c.fillRect(195, 0, 5, 5);
        }
    );
}

onload = init;