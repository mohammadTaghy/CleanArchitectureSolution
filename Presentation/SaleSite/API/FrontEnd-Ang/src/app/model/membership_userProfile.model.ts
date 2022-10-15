import { Entity } from "./base_entity/Entity";
import { Membership_User } from "./membership_user.model";

export class Membership_UserProfile extends Entity {
  public User: Membership_User;
  public Gender: string;
  public PostalCode: string;
  public PicturePath: string;
  public BirthDate: string;
  public EducationGrade: string;
  public UserDescription: string;
  public FirstName: string;
  public LastName: string;
  public NationalCode: string
}
