import 'package:json_annotation/json_annotation.dart';
part 'reservation_response.g.dart';

@JsonSerializable()
class ReservationResponse {
  int? reservationId;
  double? totalPrice;
  DateTime? arrival;
  DateTime? departure;
  String? specialRequests;
  String? estimatedArrivalTime;
  String? status;
  int? userId;
  int? roomId;

  String? roomName;
  String? userFullName;
  String? facilityNames;

  ReservationResponse();

  factory ReservationResponse.fromJson(Map<String, dynamic> json) =>
      _$ReservationResponseFromJson(json);

  /// Connect the generated [_$ReservationResponseToJson] function to the `toJson` method.
  Map<String, dynamic> toJson() => _$ReservationResponseToJson(this);
}
