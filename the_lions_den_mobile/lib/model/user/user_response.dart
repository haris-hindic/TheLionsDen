import 'package:json_annotation/json_annotation.dart';
part 'user_response.g.dart';

@JsonSerializable()
class UserResponse {
  int? userId;
  String? firstName;
  String? lastName;
  String? username;
  String? email;
  String? phoneNumber;
  String? status;
  DateTime? dateOfBirth;
  String? gender;
  int? roleId;

  String? fullName;
  String? roleName;

  UserResponse();

  factory UserResponse.fromJson(Map<String, dynamic> json) =>
      _$UserResponseFromJson(json);

  /// Connect the generated [_$UserResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$UserResponseToJson(this);
}
