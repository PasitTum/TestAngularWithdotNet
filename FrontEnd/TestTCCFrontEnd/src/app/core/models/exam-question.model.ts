export interface ExamQuestionResponse {
  id: number;
  questionNo: number;
  questionText: string;
  choiceA: string;
  choiceB: string;
  choiceC: string;
  choiceD: string;
  correctChoice: string;
  createdAt: string;
}

export interface ExamQuestionRequest {
  questionText: string;
  choiceA: string;
  choiceB: string;
  choiceC: string;
  choiceD: string;
  correctChoice: string;
}
