
// chart colors
var colors = ['#007bff', '#28a745', '#333333', '#c3e6cb', '#dc3545', '#6c757d', 'yellow'];

//Bar Chart
function createBarChart(grades, avarageGrade, attendedAvarageGrade, numberOfStudents) {

    let chBar = document.getElementById("gradesChart");
    let avarageData = setAvarageData(avarageGrade);
    let attendantAvarageData = setAvarageData(attendedAvarageGrade);

    avarageData.forEach(element => {
        element = element * numberOfStudents / 2;
    })

    attendantAvarageData.forEach(element => {
        element = element * numberOfStudents / 2;
    })

    if (chBar) {
        new Chart(chBar, {
            type: 'bar',
            data: {
                labels: ["0-54", "55-69", "70-79", "80-89", "90-94", "95-99", "100"],
                datasets: [{
                    data: [...grades],
                    backgroundColor: colors[0],
                    label: 'Students Grade'

                },
                {
                    data: [...avarageData],
                    backgroundColor: colors[4],
                    label: `Avarage Grade: ${avarageGrade}`
                },
                {
                    data: [...attendantAvarageData],
                    backgroundColor: colors[1],
                    label: `Attended Students Avarage Grade: ${attendedAvarageGrade}`
                }]
            },
            options: {
                legend: {
                    display: true,
                    position: 'top',
                    align: 'center',

                },
                scales: {
                    xAxes: [{
                        barPercentage: 0.4,
                        categoryPercentage: 0.5
                    }]
                }
            }
        });
    }

}

function setAvarageData(avarageGrade) {
    let index = 0;
    avarageData = [0, 0, 0, 0, 0, 0, 0];
    if (0 <= avarageGrade && avarageGrade <= 54) {
        avarageData[index] = 1;
    }
    index++;
    if (55 <= avarageGrade && avarageGrade <= 69) {
        avarageData[index] = 1;
    }
    index++;
    if (70 <= avarageGrade && avarageGrade <= 79) {
        avarageData[index] = 1;
    }
    index++;
    if (80 <= avarageGrade && avarageGrade <= 89) {
        avarageData[index] = 1;
    }
    index++;
    if (90 <= avarageGrade && avarageGrade <= 94) {
        avarageData[index] = 1;
    } 
    index++;
    if (95 <= avarageGrade && avarageGrade <= 99) {
        avarageData[index] = 1;
    } 
    index++;
    if (100 == avarageGrade) {
        avarageData[index] = 1;
    }
    return avarageData;
}

//-----Donuts--------
let donutOptions = {
    cutoutPercentage: 85,
    legend: { position: 'bottom', padding: 5, labels: { pointStyle: 'circle', usePointStyle: true } }
};

// AttendantPie
function createAttendencePie(numberOfStudents, studentsAttended, elementId) {
    let passFailPieData = {
        labels: ['Attended', 'Not Attended'],
        datasets: [{
            backgroundColor: colors.slice(1, 3),
            borderWidth: 0,
            data: [studentsAttended, (numberOfStudents - studentsAttended)]
        }]
    }
    createPie(passFailPieData, elementId);
}
//PassFailPie
function createPassFailPie(numberOfStudents, studentsPassed, elementId) {
    let passFailPieData = {
        labels: ['Passed', 'Failed'],
        datasets: [{
            backgroundColor: colors.slice(1, 3),
            borderWidth: 0,
            data: [studentsPassed, (numberOfStudents - studentsPassed)]
        }]
    }
    createPie(passFailPieData, elementId);
}
//GradesPie
function createGradesPie(grades, elementId) {
    let passFailPieData = {
        labels: ["0-54", "55-69", "70-79", "80-89", "90-94", "95-99", "100"],
        datasets: [{
            backgroundColor: [...colors],
            borderWidth: 0,
            data: [...grades]
        }]
    }
    createPie(passFailPieData, elementId);
}

function createPie(pieData, elementId) {
    let chPie = document.getElementById(elementId);
    if (chPie) {
        new Chart(chPie, {
            type: 'pie',
            data: pieData,
            options: donutOptions
        });
    }
}

