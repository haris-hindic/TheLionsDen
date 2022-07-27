import 'package:json_annotation/json_annotation.dart';
part 'user_insert_request.g.dart';

@JsonSerializable()
class UserInsertRequest {
  String? firstName;
  String? lastName;
  String? username;
  String? phoneNumber;
  String? email;
  String? password;
  String? passwordConfirmation;
  String? gender;
  String? dateOfBirth;
  int? roleId;

  UserInsertRequest();

  factory UserInsertRequest.fromJson(Map<String, dynamic> json) =>
      _$UserInsertRequestFromJson(json);

  /// Connect the generated [_$UserInsertRequestToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$UserInsertRequestToJson(this);
}
