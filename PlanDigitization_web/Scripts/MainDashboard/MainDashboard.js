//const { auto } = require("@popperjs/core");

function MainDashboard(URL, sURL, company, plant, line, R_url, user1) {
	this.URL = URL;
	this.sURL = sURL;
	this.company = company;
	this.plant = plant;
	this.line = line;
	this.R_url = R_url;
	this.user1 = user1;
	////debugger;
	carouselMachine();

}



//Carousel
function carouselMachine() {
	var machine = 5;

	//$.getScript("../Scripts/d3.min.js");
	////debugger;
	var URL = this.sURL;

	var R_url = this.R_url;
	var user1 = this.user1;



	var myData =
	{
		"CompanyCode": this.company,
		"PlantCode": this.plant,
		"LineCode": this.line,
	};

	$.ajax({
		type: "POST",
		url: URL + 'api/OEE/Get_NewUI_data',
		headers: {
			Authorization: 'Bearer ' + user1
		},
		data: myData,
		dataType: "json",
		success: function (response) {
			if (response.data.Table == null) {
				//console.log("No Data Available");
				$(".item_div").html(`<div class="col-md-12 item_div_nodata">Data Logging Kit(s) is not transmitting the data</div>`);
			}
			else {

				var ItemNo = (response.data.Table.length) / 2;


				var cols = "";
				var cols_btn = "";
				var cols_status = "";

				let machine_no = 0;

				for (let i = 0; i < ItemNo; i++) {



					if (i == 0) {
						cols += "<div class='item active item_" + i + "' id='darkg'>";
					} else {
						cols += "<div class='item item_" + i + "' id='darkg'>";
					}

					cols += "<div class='panel panel-default panel0' id='darkg'>";
					cols += "<div class='panel-body ' id='darkg'>";
					cols += "<div class='row' id='darkg'>";
					cols += "<div class='col-md-12' id='darkg'>";
					cols += "<div class='row row_slide'  id='darkg'>";

					for (let jj = 0; jj < 2; jj++) {

						if (machine_no < response.data.Table.length) {
							;

							var machine_nos = machine_no + 1;
							var MTTR = '-';
							var Min = '';
							if (response.data.Table[machine_no].MTTR != 0) {
								MTTR = response.data.Table[machine_no].MTTR;
								Min = "mins";
							}

							var MTBF = '-';
							var Min_MTBF = '';
							if (response.data.Table[machine_no].MTBF != 0) {
								MTBF = response.data.Table[machine_no].MTBF;
								Min_MTBF = "mins";
							}
							var Station_Number = response.data.Table[machine_no].Machine_code.substring(1);
							//console.log("Station Number:" + Station_Number);


							cols += `<div class='col-md-6'>
									<section class='panel panel1'  >
										<header class='panel-heading'>
											<div class='row'>
												<div class='col-md-12 fontSanserif' >
													Station  ${Station_Number} - ${response.data.Table[machine_no].AssetDescription}
													<li class="breadcrumb-item titleTooltip">
														<a href="#" class="dropdown-toggle notification-icon" data-toggle="dropdown">
															<i class="fa fa-square " id="carouselMachineStatus${machine_nos}"></i>
															<span class="tooltiptext2" id="carouselMachineStatusText${machine_nos}">Latest Alarm</span>
														</a>
													</li>
													<div class="right-wrapper pull-right">
														<ol class="breadcrumb">
																<li class="breadcrumb-item titleTooltip">
																	<a href="#" class="dropdown-toggle notification-icon" data-toggle="dropdown">
																		<i class="fa fa-clock-o text-primary"></i>
																		<span class="tooltiptext">Hourly Tracker</span>
																	</a>
																</li>
															<li class="breadcrumb-item titleTooltip">
																<a href="#" class="dropdown-toggle notification-icon" data-toggle="dropdown">
																	<i class="fa fa-bar-chart-o text-muted"></i>
																	<span class="tooltiptext">KPI</span>
																</a>
															</li>
														</ol>
													</div>
												</div>
											</div>
											<div class="row">
												<div class='col-md-12'>
													<div id=' ' class='machineBatchcode'> ${response.data.Table[machine_no].Batch}
														<div class="right-wrapper pull-right">
															<ol class="breadcrumb">
																<li class="breadcrumb-item titleTooltip" >
																	<a href="#" class="dropdown-toggle notification-icon" data-toggle="dropdown" >
																		<i class="fa fa-bell-o text-danger"></i>
																		<span class="tooltiptext">Latest Alarm</span>
																	</a>
																</li>
																<li class="breadcrumb-item titleTooltip" >
																	<a href="#" class="dropdown-toggle notification-icon" data-toggle="dropdown" >
																		<i class="fa fa-exclamation-triangle text-warning"></i>
																		<span class="tooltiptext">Latest Loss</span>
																	</a>
																</li>
																<li class="breadcrumb-item titleTooltip">
																	<a href="#" class="dropdown-toggle notification-icon" data-toggle="dropdown">
																		<i class="fa fa-thumbs-o-down text-secondary"></i>
																		<span class="tooltiptext">Latest Rejection</span>
																	</a>
																</li>
																
															</ol>
														</div>
													</div>
												</div>
											</div>
										</header>
										<div class='panel-body' >
											<div class='row'>
												<div class='col-md-4 kpiCard'>
													<div class='col-md-12 kpiCard_outer'>
														<div class='row row_shift'>
															<div class='col-md-12 kpicard_Shift'>
																<div>Shift :
																	<span> ${response.data.Table[machine_no].Shift_id} </span>
																</div>
															</div>
														</div>
														<div class='row row_oee'>
															<center>
																<div class='widget_${machine_nos} carouselWidget oee'></div>
															</center>
														</div>
														<div class='row row_apq' id='bm'>
															<div class='container'>
																<progress id='file' value=${response.data.Table[machine_no].Availability} max='100'></progress>
																	&nbsp;&nbsp;
																	<span><b>A:</b>
																		<span class="kpicard_apq_value">${response.data.Table[machine_no].Availability}%</span>
																	</span>
																<br />
																<progress id='file' value=${response.data.Table[machine_no].Performance} max='100'></progress>
																	&nbsp;&nbsp;
																	<span><b>P:</b>
																		<span class="kpicard_apq_value">${response.data.Table[machine_no].Performance}%</span>
																	</span>
																<br />
																<progress id='file' value=${response.data.Table[machine_no].Quality} max='100'></progress>
																	&nbsp;&nbsp;
																	<span><b>Q:</b>
																		<span class="kpicard_apq_value">${response.data.Table[machine_no].Quality}%</span>
																	</span>
															</div>
														</div>
													</div>
												</div>
												<div class='col-md-8 productionCard' id='bm'>
													<div class='col-md-12 productionCard prodCardMargin'>
														<div class='row row_ProdCard' id='bm' >
															<div class="carousel_inner_padding">
																<div class='col-md-3 '>
																	<div class='row'>
																		<div class='col-md-12 '>
																			<div class='col-md-4 nopadding'>
																				<span class="carousel_Label">OK</span>
																			</div>
																			<div class='col-md-8 noRpadding'>
																				<span class="carousel_Value">${response.data.Table[machine_no].OK_Parts}</span>
																			</div>
																		</div>
																	</div>
																	<div class='row'>
																		<div class='col-md-12'>
																			<div class='col-md-4 nopadding'>
																				<span class="carousel_Label">NOK</span>
																			</div>
																			<div class='col-md-8 noRpadding'>
																				<span class="carousel_Value">${response.data.Table[machine_no].NOK_Parts}</span>
																			</div>
																		</div>
																	</div>
																</div>
																<div class='col-md-6'>
																	<div class='row centreText'>
																		<span class="carousel_Label">VARIANT</span>
																	</div>
																	<div class='row centreText'>
																		<span class="carousel_Value">${response.data.Table[machine_no].Variant}
																		</span>
																	</div>
																	<div class="mycontent-left"> </div>
																</div>
																<div class='col-md-3'>
																	<div class='row'>
																		<div class='col-md-12 '>
																			<div class='col-md-4 nopadding'>
																				<span class="carousel_Label">CT</span>
																			</div>
																			<div class='col-md-8 noRpadding'>
																				<span class="carousel_Value">${response.data.Table[machine_no].Cycletime}s
																				</span>
																			</div>
																		</div>
																	</div>
																	<div class='row'>
																		<div class='col-md-12 '>
																			<div class='col-md-4 nopadding'>
																				<span class="carousel_Label">TCT</span>
																			</div>
																			<div class='col-md-8 noRpadding'>
																				<span class="carousel_Value">${response.data.Table[machine_no].TCycletime}s</span>
																			</div>
																		</div>
																	</div>
																</div>
															</div>
														</div>
														<div class='row row_alarm' id='bm'>
															<div class="carousel_inner_padding">
																<div class='col-md-4 centreText'>
																	<ul class="notifications">
																		<li class="titleTooltip">
																			<a href="#" class="dropdown-toggle notification-icon no_hand_cursor" data-toggle="dropdown">
																				<i class="fa fa-bell"></i>
																				<span class="badge">${response.data.Table[machine_no].LiveAlarmCounts}</span>
																				<span class="tooltiptext">
																					Total Stoppage : ${response.data.Table[machine_no].No_of_stopage}</br>
																					Live Alarms : ${response.data.Table[machine_no].LiveAlarmCounts}
																				</span>
																			</a>
																		</li>
																	</ul>
																</div>
																<div class='col-md-4'>
																	<div class='row centreText'>
																		<span class="carousel_Label">MTTR</span>
																	</div>
																	<div class='row centreText'>
																		<span class="carousel_Value">${MTTR}
																			<span class="row_alarm_Min">${Min}</span>
																		</span>
																	</div>
																</div>
																<div class='col-md-4'>
																	<div class='row centreText'>
																		<span class="carousel_Label">MTBF</span>
																	</div>
																	<div class='row centreText'>
																		<span class="carousel_Value">${MTBF}
																			<span class="row_alarm_Min">${Min_MTBF}</span>
																		</span>
																	</div>
																</div>
															</div>
														</div>
														<div class='row zoom row_util' id='bm'>
															<div class='col-md-12'>
																<div class="util_overview" id='utli_overview_${response.data.Table[machine_no].Machine_code}' ></div>
															</div>
														</div>

													</div>
												</div>
											</div>
										</div>
									</section>
								</div>`;


							cols_status += `<div class='flex-item-status_bar_box titleTooltip_status machine_status_bars' id="carouselStatusBar${machine_nos}" >
												<label id="statusbar_hov">${response.data.Table[machine_no].Machine_code}</label><br> 
												<label id="okpart_id">${response.data.Table[machine_no].OK_Parts}</label>/
												<label id="nok_id">${response.data.Table[machine_no].NOK_Parts}</label>`;
							cols_status += `<span class="tooltiptext_status" id="statusbar_toltip${machine_nos}"></span>
											</div>`;

							machine_no++;




						}


					}



					cols += "</div>";
					cols += "</div>";
					cols += "</div>";
					cols += "</div>";
					cols += "</div>";
					cols += "</div>";


					let ii = 2;
					if (i == 0) {
						//cols_btn += "<li data-target='#myCarousel' data-slide-to='" + i + "' title='Machine Status Bar  Station 1 - 2' class='active' style='background-color: darkgrey; color: darkgrey; '></li>";
						cols_btn += `<li data-target='#myCarousel' data-slide-to='${i}' title='Machine Status Bar  Station 1 - 2' class='active' id="slide_nav_button"></li>`;
					} else {
						var st1 = (i + (ii));
						var st2 = (i + (ii + 1));
						//cols_btn += "<li data-target='#myCarousel' data-slide-to='" + i + "' title='Machine Status Bar  Station " + st1 + " - " + st2 + "' class='car_nav_list_" + i + "' style='background-color: darkgrey; color: darkgrey; '></li>";
						cols_btn += `<li data-target='#myCarousel' data-slide-to='${i}' title='Machine Status Bar  Station ${st1} - ${st2}' class='car_nav_list_${i}' id="slide_nav_button"></li>`;
					}


				}

				$(".item_div").html(cols);

				$("#carousel_btn").html(cols_btn);
				$(".statusbar_flex").html(cols_status);
				let machine_no1 = 0;

				for (let i1 = 0; i1 < ItemNo; i1++) {
					for (let jj1 = 0; jj1 < 2; jj1++) {
						if (machine_no1 < response.data.Table.length) {

							//-----------Utilization Chart-----------------//

							var data = [
								{ name: "Production Time", value: response.data.Table[machine_no1].Uptime },
								{ name: "Loss Time", value: response.data.Table[machine_no1].Losstime },
								{ name: "Down Time", value: response.data.Table[machine_no1].Downtime },
								{ name: "Break Time", value: response.data.Table[machine_no1].Breaktime },

							];

							var totalCal = response.data.Table[machine_no1].Uptime + response.data.Table[machine_no1].Losstime + response.data.Table[machine_no1].Downtime + response.data.Table[machine_no1].Breaktime;
							var text = "";
							var width = 90;
							var height = 90;
							var thickness = 20;
							var duration = 750;
							var padding = 2;
							var opacity = 0.8;
							var opacityHover = 1;
							var otherOpacityOnHover = 0.8;
							var tooltipMargin = 2;

							var radius = Math.min(width, height) / 2;
							var color = d3.scaleOrdinal(['#87c300', '#fac316 ', '#F39C12', '#3d85c6']);

							var svg = d3.select("#utli_overview_" + response.data.Table[machine_no1].Machine_code)
								.append('svg')
								.attr('class', 'pie')
								.attr('width', width)
								.attr('height', height)
								.style('margin-top', '18px')
								.style('margin-left', '5px');

							var g = svg.append('g')
								.attr('transform', 'translate(' + (height / 2) + ',' + (height / 2) + ')');

							var arc = d3.arc()
								.innerRadius(0)
								.outerRadius(radius);

							var pie = d3.pie()
								.value(function (d) { return d.value; })
								.sort(null);

							var path = g.selectAll('path')
								.data(pie(data))
								.enter()
								.append("g")
								.append('path')
								.attr('d', arc)
								.attr('fill', (d, i) => color(i))
								.style('opacity', opacity)
								.style('stroke', 'white')

								.on("mouseout", function (d) {
									d3.select("svg")
										.style("cursor", "none")
										.select(".tooltip").remove();
									d3.selectAll('path')
										.style("opacity", opacity);
								})
								.on("touchstart", function (d) {
									d3.select("svg")
										.style("cursor", "none");
								})
								.each(function (d, i) { this._current = i; });

							let legend = d3.select("#utli_overview_" + response.data.Table[machine_no1].Machine_code).append('div')
								.attr('class', 'legend1')
								.style('margin-top', '-105px');

							legend.append("text")
								.text("UTILISATION")
								.style("color", "grey")
								.style("font-size", "10px");

							let keys = legend.selectAll('.key')
								.data(data)
								.enter().append('div')
								.attr('class', 'key')
								.style('display', 'flex')
								.style('align-items', 'center')
								.style('height', '13px')
								.style('margin-right', '40px');

							keys.append('div')
								.attr('class', 'symbol')
								.style('height', '7px')
								.style('width', '7px')
								.style('margin', '0px 10px 1px 1px')
								.style('background-color', (d, i) => color(i));

							keys.append('div')
								.attr('class', 'name')
								.text(d => `${d.name} - ${d.value} (mins) - ${(Math.round((d.value * 100) / totalCal))}%`);

							keys.exit().remove();



							//-----------Circular Bar Chart-----------------//
							function radialProgress(selector, labelText) {
								const parent = d3.select(selector)

								const size = { top: 0, right: 0, bottom: 0, left: 0, height: 117, width: 117, x: 10, y: 10 };

								d3.select(selector + " svg").remove();
								const svg = parent.append('svg');
								//	.attr('width', 199)
								//	.attr('height', 199);

								const outerRadius = Math.min(size.width, size.height) * 0.45;//radius
								const thickness = 8; //bar thickness
								let value = 0;

								const mainArc = d3.arc()
									.startAngle(0)
									.endAngle(Math.PI * 2)
									.innerRadius(outerRadius - thickness)
									.outerRadius(outerRadius)

								svg.append("path")
									.attr('class', 'progress-bar-bg')
									.attr('transform', `translate(${size.width / 2},${size.height / 2})`)
									.attr('d', mainArc())

								const mainArcPath = svg.append("path")
									.attr('class', 'progress-bar')
									.attr('transform', `translate(${size.width / 2},${size.height / 2})`)

								svg.append("circle")
									.attr('class', 'progress-bar')
									.attr('transform', `translate(${size.width / 2},${size.height / 2 - outerRadius + thickness / 2})`)
									.attr('width', thickness)
									.attr('height', thickness)
									.attr('r', thickness / 2)

								const end = svg.append("circle")
									.attr('class', 'progress-bar')
									.attr('transform', `translate(${size.width / 2},${size.height / 2 - outerRadius + thickness / 2})`)
									.attr('width', thickness)
									.attr('height', thickness)
									.attr('r', thickness / 2)

								let percentLabel = svg.append("text")
									.attr('class', 'progress-label')
									.attr('transform', `translate(${size.width / 2},${((size.height / 2) - (size.height / 25))})`)
									.text('0')
								let percentLabel1 = svg.append("text")
									.attr('class', 'progress-label1')
									.attr('transform', `translate(${size.width / 2},${((size.height / 2) + (size.height / 6))})`)
									.text('0')
								return {
									update: function (progressPercent) {
										const startValue = value
										const startAngle = Math.PI * startValue / 50
										const angleDiff = Math.PI * progressPercent / 50 - startAngle;
										const startAngleDeg = startAngle / Math.PI * 180
										const angleDiffDeg = angleDiff / Math.PI * 180
										const transitionDuration = 1500

										mainArcPath.transition().duration(transitionDuration).attrTween('d', function () {
											return function (t) {
												mainArc.endAngle(startAngle + angleDiff * t)
												return mainArc();
											}
										})
										end.transition().duration(transitionDuration).attrTween('transform', function () {
											return function (t) {
												return `translate(${size.width / 2},${size.height / 2})` +
													`rotate(${(startAngleDeg + angleDiffDeg * t)})` +
													`translate(0,-${outerRadius - thickness / 2})`
											}
										})
										percentLabel.transition().duration(transitionDuration).tween('bla', function () {
											return function (t) {
												percentLabel.text((Math.round(startValue + (progressPercent - startValue) * t)) + " %");
												percentLabel1.text(labelText);
											}
										})
										value = progressPercent
									}
								}
							}


							let chart = radialProgress(('.widget_' + (machine_no1 + 1)), 'OEE')
							let progress = [response.data.Table[machine_no1].OEE]
							let state = 0
							chart.update(progress[state])
							state = (state + 1) % progress.length


							var machineStatusColor = '';
							var machineStatusText = '';
							if (response.data.Table1[machine_no1].Machine_Status == "0") {
								machineStatusColor = '#D21312';
								machineStatusText = 'M/C under Error';
							}
							if (response.data.Table1[machine_no1].Machine_Status == '1') {
								machineStatusColor = '#82CD47';
								machineStatusText = 'M/C Running';
							}
							if (response.data.Table1[machine_no1].Machine_Status == '2') {
								machineStatusColor = '#FFD93D';
								machineStatusText = 'Machine is Idle/Loss';
							}
							if (response.data.Table1[machine_no1].Machine_Status == '3') {
								machineStatusColor = '#FFD93D';
								machineStatusText = 'Machine is Idle/Loss';
							}
							if (response.data.Table1[machine_no1].Machine_Status == '4') {
								machineStatusColor = '#5B8FB9';
								machineStatusText = 'Planned Break';
							}
							if (response.data.Table1[machine_no1].Machine_Status == '5') {
								machineStatusColor = '#D8D8D8';
								machineStatusText = 'PLC disconnect with Gateway';
							}
							var carouselMachineStatus = document.getElementById('carouselMachineStatus' + (machine_no1 + 1));
							var carouselStatusBar = document.getElementById('carouselStatusBar' + (machine_no1 + 1));
							var carouselMachineStatusText = document.getElementById('carouselMachineStatusText' + (machine_no1 + 1));
							var statusbarToolTip = document.getElementById('statusbar_toltip' + (machine_no1 + 1));

							carouselMachineStatus.style.color = machineStatusColor;
							carouselMachineStatusText.innerHTML = machineStatusText;
							carouselStatusBar.style.backgroundColor = machineStatusColor;
							//statusbarToolTip.innerHTML = "Station Name : " + "<b>" + response.data.Table[machine_no1].AssetDescription + "</b><br>" + "Status : <b>" + machineStatusText + "</b><br>OEE : <b>" + progress + " %</b>";
							statusbarToolTip.innerHTML = `${response.data.Table[machine_no1].AssetDescription}<br>Status : <b>${machineStatusText}</b><br>OEE : <b>${progress}%</b>`;

							machine_no1++;
						}

					}

				}


			}

		},
		error: function (response) {
			if (response.status == "401") {
				swal({
					icon: "warning",
					title: "Session Timeout",
					button: true,
					closeModal: false
				})
				window.location = R_url;
			}
			else {
				swal({
					icon: "warning",
					title: response.responseText,
					button: true,
					closeModal: false
				})

			}
		}
	});



	setTimeout(carouselMachine, 60000);


}



//--Circular progress bar chart for popup--//
function radialProgress(selector, labelText) {
	const parent = d3.select(selector)

	const size = { top: 0, right: 0, bottom: 0, left: 0, height: 175, width: 175, x: 10, y: 10 };

	d3.select(selector + " svg").remove();
	const svg = parent.append('svg')
		.attr('width', size.width)
		.attr('height', size.height);

	const outerRadius = Math.min(size.width, size.height) * 0.45;//radius
	const thickness = 10; //bar thickness
	let value = 0;

	const mainArc = d3.arc()
		.startAngle(0)
		.endAngle(Math.PI * 2)
		.innerRadius(outerRadius - thickness)
		.outerRadius(outerRadius)

	svg.append("path")
		.attr('class', 'progress-bar-bg')
		.attr('transform', `translate(${size.width / 2},${size.height / 2})`)
		.attr('d', mainArc())

	const mainArcPath = svg.append("path")
		.attr('class', 'progress-bar')
		.attr('transform', `translate(${size.width / 2},${size.height / 2})`)

	svg.append("circle")
		.attr('class', 'progress-bar')
		.attr('transform', `translate(${size.width / 2},${size.height / 2 - outerRadius + thickness / 2})`)
		.attr('width', thickness)
		.attr('height', thickness)
		.attr('r', thickness / 2)

	const end = svg.append("circle")
		.attr('class', 'progress-bar')
		.attr('transform', `translate(${size.width / 2},${size.height / 2 - outerRadius + thickness / 2})`)
		.attr('width', thickness)
		.attr('height', thickness)
		.attr('r', thickness / 2)

	let percentLabel = svg.append("text")
		.attr('class', 'progress-label')
		.attr('transform', `translate(${size.width / 2},${((size.height / 2) - (size.height / 25))})`)
		.text('0')
	let percentLabel1 = svg.append("text")
		.attr('class', 'progress-label1')
		.attr('transform', `translate(${size.width / 2},${((size.height / 2) + (size.height / 6))})`)
		.text('0')
	return {
		update: function (progressPercent) {
			const startValue = value
			const startAngle = Math.PI * startValue / 50
			const angleDiff = Math.PI * progressPercent / 50 - startAngle;
			const startAngleDeg = startAngle / Math.PI * 180
			const angleDiffDeg = angleDiff / Math.PI * 180
			const transitionDuration = 1500

			mainArcPath.transition().duration(transitionDuration).attrTween('d', function () {
				return function (t) {
					mainArc.endAngle(startAngle + angleDiff * t)
					return mainArc();
				}
			})
			end.transition().duration(transitionDuration).attrTween('transform', function () {
				return function (t) {
					return `translate(${size.width / 2},${size.height / 2})` +
						`rotate(${(startAngleDeg + angleDiffDeg * t)})` +
						`translate(0,-${outerRadius - thickness / 2})`
				}
			})
			percentLabel.transition().duration(transitionDuration).tween('bla', function () {
				return function (t) {
					percentLabel.text((Math.round(startValue + (progressPercent - startValue) * t)) + " %");
					percentLabel1.text(labelText);
				}
			})
			value = progressPercent
		}
	}
}



