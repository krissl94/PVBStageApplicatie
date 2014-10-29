function init(id) {
    var mouseIsDown = false, last = {}, canvas;
    
    if (id) {
        canvas = d3.select("#" + id)
        last[id] = {};
    }
    else {
        canvas = d3.selectAll(".cvs").each(function () {
            last[this.id] = {};
        });
    }

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
}

function ClearCanvas(id){
    var parent = d3.select(d3.select("#" + id).node().parentNode);

    d3.select("#" + id).remove();

    parent.append("canvas")
        .attr("id", id)
        .attr("width", 200)
        .attr("height", 100)
        .attr("class", "cvs");

    init(id);
}

onload = init();