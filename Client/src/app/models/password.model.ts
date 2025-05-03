export interface PasswordRequestModel {
    password: string;
  }
  
  export interface PasswordResponseModel {
    isStrong: boolean;
    isCompromised: boolean;
    strengthFeedback: string;
    compromisedFeedback:string;
  }
  