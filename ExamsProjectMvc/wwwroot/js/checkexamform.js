function checkForm(event) {
    let questionsArr = [];

    let isValid = true;
    let questionsDiv = document.getElementById("questions");
    let questionsCards = questionsDiv.querySelectorAll(".card")


    questionsCards.forEach(element => {
        let questionObject = {};

        if (element.classList.contains("border-danger")) {
            element.classList.remove("border-danger")
            element.classList.add("border-dark")
            let invalidFooter = element.querySelector("#invalidFooter");
            if (invalidFooter !== null) {
                invalidFooter.remove();
            }
        }
        let questionTextElement = element.querySelector("#questionInputText");
        let questionText = questionTextElement.value
        if (questionText == null || questionText == "") {
            isValid = false;
            questionTextElement.classList.add("is-invalid");
            element.classList.add("border-danger")
            element.classList.remove("border-dark")
        } else {
            if (questionTextElement.classList.contains("is-invalid")) {
                questionTextElement.classList.remove("is-invalid");
            }
        }
        let selectedRadio = element.querySelector("input[type=radio]:checked");
        let correctAnswerText = "";
        if (selectedRadio == null) {
            isValid = false;
            element.classList.add("border-danger");
            element.classList.remove("border-dark");
            let invalidFooter = document.createElement("div");
            invalidFooter.id = "invalidFooter";
            invalidFooter.className = "card-footer text-white bg-danger";

            invalidFooter.innerHTML = "*Please select the correct answer";
            element.appendChild(invalidFooter);

        } else {
            let correctAnswerValue = selectedRadio.value;
            correctAnswerText = element.querySelector(`#optionText${correctAnswerValue}`).value;

        }
        let answerChoicesText = [];
        let answerChoicesInputElements = element.querySelectorAll("input[type=text]");
        for (var i = 1; i < answerChoicesInputElements.length - 1; i++) {
            let inputElement = answerChoicesInputElements[i]
            let inputValue = inputElement.value
            if (inputValue != null && inputValue != "") {
                answerChoicesText.push(inputValue);
                if (inputElement.classList.contains("is-invalid")) {
                    inputElement.classList.remove("is-invalid");
                }
            }
            else {
                isValid = false;
                element.classList.add("border-danger");
                element.classList.remove("border-dark");
                answerChoicesInputElements[i].classList.add('is-invalid');
            }
        }

        questionObject = {
            AnswerChoises: [...answerChoicesText],
            CorrectAnswerText: correctAnswerText,
            QuestionText: questionText
        };

        //Create question object -> insert into questions array
        questionsArr.push(questionObject);
    });
    if (isValid) {
        //submit
        console.log(questionsArr);

        //  <input type="hidden" name="yourmodelprefix[0].PlanID" />
        let hiddenDiv = document.getElementById("hidden");
        questionsArr.forEach(function (question, index) {
            let questionText = document.createElement("input");
            questionText.type = "hidden";
            questionText.value = question.QuestionText
            questionText.name = `Questions[${index}].QuestionText`;
            hiddenDiv.appendChild(questionText);

            let questionCorrectAnsewr = document.createElement("input");
            questionCorrectAnsewr.type = "hidden";
            questionCorrectAnsewr.value = question.CorrectAnswerText
            questionCorrectAnsewr.name = `Questions[${index}].CorrectAnswerText`;
            hiddenDiv.appendChild(questionCorrectAnsewr);

            question.AnswerChoises.forEach(function (answer, i) {
                let questionAnswerChoiseText = document.createElement("input");
                questionAnswerChoiseText.type = "hidden";
                questionAnswerChoiseText.value = answer;
                questionAnswerChoiseText.name = `Questions[${index}].AnswerChoises[${i}].AnswerChoiceText`;
                hiddenDiv.appendChild(questionAnswerChoiseText);

            })

        })

        let classroomIdElement = document.getElementById("ClassroomID");
        let classroomSelectElement = document.getElementById("classroomSelect");
        classroomIdElement.value = classroomSelectElement.value;
        //      event.preventDefault();
    } else {
        event.preventDefault();
    }
}