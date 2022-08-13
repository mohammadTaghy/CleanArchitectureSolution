import { User } from "./user.model";

export class UserProfile {
  public User: User;
  public constructor(public Gender: string, public PostalCode: string, public PicturePath: string, public BirthDate: string,
    public EducationGrade: string, public UserDescription: string, public FirstName: string, public LastName: string,
    public NationalCode: string,  ) { }
}
